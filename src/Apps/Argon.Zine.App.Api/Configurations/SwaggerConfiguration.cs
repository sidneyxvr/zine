using Microsoft.OpenApi.Models;

namespace Argon.Zine.App.Api.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection RegisterSwagger(this IServiceCollection services, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type =>
                {
                    var t = type.ToString().Split('.');

                    var service = t.ElementAt(1);

                    return service.Equals(nameof(Core)) ? $"Common-{t.Last()}" : $"{t.ElementAt(1)}-{t.Last()}";
                });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Argon.Zine.App.Api", Version = "v1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "Argon.Zine.App.Api", Version = "v2" });
            });
        }

        return services;
    }

    public static IApplicationBuilder UseSwaggerR(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Argon.Zine.App.Api v1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "Argon.Zine.App.Api v2");
        });

        return app;
    }
}