using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Argon.WebApp.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Argon.WebApp.API", Version = "v1" });
                    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Argon.WebApp.API", Version = "v2" });
                });
            }

            return services;
        }

        public static IApplicationBuilder UseSwaggerR(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Argon.WebApp.API v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Argon.WebApp.API v2");
            });

            return app;
        }
    }
}
