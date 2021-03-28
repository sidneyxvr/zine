using Argon.Customers.Infra.Data;
using Argon.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Argon.WebApp.API.Configurations
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection RegisterDbContexts(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                return RegisterDevelopmentContexts(services, configuration);
            }
            else if (env.IsProduction())
            {
                return RegisterProductionContexts(services, configuration);
            }

            return services;
        }

        private static IServiceCollection RegisterDevelopmentContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"))
                    .LogTo(Console.WriteLine)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());

            services.AddDbContext<CustomerContext>(options =>
               options
                   .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                       x => x.UseNetTopologySuite())
                       .LogTo(Console.WriteLine)
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());

            return services;
        }

        private static IServiceCollection RegisterProductionContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<CustomerContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.UseNetTopologySuite()));

            return services;
        }
    }
}
