using Argon.Zine.App.Api.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;
using System.Diagnostics;
using System.Reflection;

namespace Argon.Zine.App.Api.Configurations;

public static class TracingConfiguration
{
    private static readonly string[] s_ignoreRoutes =
        { "/metrics", "/health", "/swagger", "/health-ui-api", "/health-ui", "/favicon.ico", "/ui" };

    public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration configuration)
    {
        var jaegerSettingsSection = configuration.GetSection(nameof(OpenTelemetrySettings));
        var jaegerSettings = jaegerSettingsSection.Get<OpenTelemetrySettings>();
        services.Configure<OpenTelemetrySettings>(jaegerSettingsSection);

        if (jaegerSettings?.Enable == false)
        {
            return services;
        }

        services.AddOpenTelemetryTracing(builder =>
        {
            var connection = ConnectionMultiplexer.Connect(
                configuration.GetConnectionString("CatalogRedis"));

            builder.AddJaegerExporter(options => options.AgentHost = jaegerSettings!.HostName)
                .AddSource(typeof(Startup).Assembly.GetName().Name)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WebApi"))
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = (httpContext)
                        => !s_ignoreRoutes.Any(r => httpContext.Request.Path.StartsWithSegments(r));
                    options.Enrich = AspNetCoreEnrich;
                    options.RecordException = true;
                })
                .AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true)
                .AddMongoDBInstrumentation()
                .Configure((sp, builder) =>
                {
                    var cache = (RedisCache)sp.GetRequiredService<IDistributedCache>();
                    builder.AddRedisInstrumentation(cache.GetConnection());
                })
                .AddXRayTraceId()
                .AddXRayTraceIdWithSampler(new AlwaysOnSampler())
                .AddAWSInstrumentation();
        });

        return services;
    }

    public static ConnectionMultiplexer GetConnection(this RedisCache cache)
    {
        typeof(RedisCache).InvokeMember("Connect",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
            null, cache, Array.Empty<object>());

        var field = typeof(RedisCache).GetField("_connection", BindingFlags.Instance | BindingFlags.NonPublic);
        var connection = (ConnectionMultiplexer)field!.GetValue(cache)!;
        return connection;
    }

    private static void AspNetCoreEnrich(Activity activity, string eventName, object obj)
    {
        if (obj is HttpRequest request)
        {
            var context = request.HttpContext;
            activity.AddTag("http.scheme", request.Scheme);
            activity.AddTag("http.client_ip", context.Connection.RemoteIpAddress);
            activity.AddTag("http.request_content_length", request.ContentLength);
            activity.AddTag("http.request_content_type", request.ContentType);
            activity.AddTag("http.request_id", context.TraceIdentifier);

            var user = context.User;
            if (user.Identity?.Name is not null)
            {
                activity.AddTag("enduser.id", user.Identity.Name);
                activity.AddTag(
                    "enduser.scope",
                    string.Join(',', user.Claims.Select(x => x.Value)));
            }
        }
        else if (obj is HttpResponse response)
        {
            activity.AddTag("http.response_content_length", response.ContentLength);
            activity.AddTag("http.response_content_type", response.ContentType);
        }
    }
}