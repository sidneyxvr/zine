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

        app.UseMetricServer("/metrics");
        app.UseHttpMetrics();

        return app;
    }
}