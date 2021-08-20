using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Argon.App.Api
{
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
                //.UseSerilog((context, configuration) =>
                //{
                //    if (context.HostingEnvironment.IsProduction())
                //    {
                //        var credentials = new NoAuthCredentials("http://localhost:3100");

                //        configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //            .Enrich.FromLogContext()
                //            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                //            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                //            .WriteTo.LokiHttp(credentials);
                //    }
                //})
            ;
    }
}
