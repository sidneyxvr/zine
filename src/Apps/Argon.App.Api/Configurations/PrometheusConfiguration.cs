using Microsoft.AspNetCore.Builder;
using Prometheus;

namespace Argon.App.Api.Configurations
{
    public static class PrometheusConfiguration
    {
        public static IApplicationBuilder UsePrometheus(this IApplicationBuilder app)
        {
            var counter = Metrics.CreateCounter("webapi_path_counter", "Counts requests to the WEB API endpoints", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });
            app.Use((context, next) =>
            {
                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
                return next();
            });

            app.UseMetricServer();
            app.UseHttpMetrics();

            return app;
        }
    }
}
