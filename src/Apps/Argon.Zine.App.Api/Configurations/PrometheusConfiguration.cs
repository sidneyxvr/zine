using Prometheus;

namespace Argon.Zine.App.Api.Configurations;

public static class PrometheusConfiguration
{
    public static IApplicationBuilder UsePrometheus(this IApplicationBuilder app)
    {
        var counter = Metrics.CreateCounter("webapi_path_counter", "Counts requests to the WEB API endpoints",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });

        app.Use((context, next) =>
        {
            counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
            return next();
        });

        app.Map("/metrics", app =>
        {
            app.Use(async (context, next) =>
            {
                var teste = context.Request.Headers["teste"];

                if(teste.Count == 0)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                await next();
            });

            app.UseMetricServer("");
        });

        app.UseHttpMetrics();

        return app;
    }
}