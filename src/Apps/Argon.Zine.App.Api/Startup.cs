using Argon.Zine.App.Api.Configurations;
using Argon.Zine.App.Api.Hubs;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Argon.Zine.App.Api;

public class Startup
{
    public Startup(IWebHostEnvironment environment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (!environment.IsProduction())
        {
            builder.AddUserSecrets<Startup>();
        }

        Configuration = builder.Build();

        Environment = environment;
    }

    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        const string ptBRCulture = "pt-BR";

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo(ptBRCulture),
                new CultureInfo("en-US"),
            };

            options.DefaultRequestCulture = new RequestCulture(culture: ptBRCulture, uiCulture: ptBRCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(context
                => Task.FromResult(new ProviderCultureResult(ptBRCulture))!));
        });

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.RegisterHealthChecks(Configuration);
        services.RegisterCatalog(Configuration)
            .RegisterCustomer()
            .RegisterSupplier()
            .RegisterOrdering()
            .RegisterChat()
            .RegisterIdentity(Configuration, Environment)
            .RegisterJwt(Configuration)
            .RegisterDbContexts(Configuration, Environment)
            .RegisterServices(Configuration);

        services.AddSignalR(options => options.EnableDetailedErrors = true)
            .AddMessagePackProtocol();

        services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddCors();

        services.RegisterSwagger(Environment);

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddTracing(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerR();
        }
        app.UseSerilogRequestLogging();
        app.UsePrometheus();

        var supportedCultures = new[] { "en-US", "pt-BR" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

        app.UseRequestLocalization(localizationOptions);

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatHub>("/chathub");
        });

        app.UseHealthChecks();
    }
}