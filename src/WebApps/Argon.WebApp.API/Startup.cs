using Argon.WebApp.API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Globalization;
using System.Threading.Tasks;

namespace Argon.WebApp.API
{
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

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo(ptBRCulture),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: ptBRCulture, uiCulture: ptBRCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(context 
                    => Task.FromResult(new ProviderCultureResult("pt"))!));
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.RegisterCustomer();
            services.RegisterSupplier();
            services.RegisterIdentity(Environment);
            services.RegisterJwt(Configuration);
            services.RegisterDbContexts(Configuration, Environment);

            services.AddControllers();

            services.RegisterSwagger(Environment);
            services.RegisterServices(Configuration);

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerR();
            }
            else if (env.IsProduction())
            {
                app.UseSerilogRequestLogging();

                app.UsePrometheus();
            }
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                ApplyCurrentCultureToResponseHeaders = true
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
