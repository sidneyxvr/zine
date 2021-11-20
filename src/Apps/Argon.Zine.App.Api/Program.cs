using Serilog;
using Serilog.Events;
using Serilog.Sinks.Loki;

namespace Argon.Zine.App.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((context, configuration) =>
            {
                if (context.HostingEnvironment.IsProduction())
                {
                    configuration
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .WriteTo.LokiHttp(() => new LokiSinkConfiguration { LokiUrl = "http://localhost:3100" });
                }
            })
        ;
}