using Microsoft.OpenApi.Models;

namespace Argon.Zine.App.Api.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection RegisterSwagger(this IServiceCollection services, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            return services;
        }
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type 
                => type.FullName!.StartsWith(nameof(Commom))
                    ? $"Common.{type.Name}"
                    : $"{type.FullName.Split('.')[2]}.{type.Name}");

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                },
            });

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Argon.Zine.App.Api", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "Argon.Zine.App.Api", Version = "v2" });
        });

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