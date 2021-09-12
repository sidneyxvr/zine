using Argon.Zine.Basket.Data;
using Argon.Zine.Catalog.Infra.Data;
using Argon.Zine.Catalog.Infra.Data.Queries;
using Argon.Zine.Chat.Data;
using Argon.Zine.Customers.Infra.Data;
using Argon.Zine.Identity.Data;
using Argon.Zine.Ordering.Infra.Data;
using Argon.Restaurants.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Argon.Zine.App.Api.Configurations
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection RegisterDbContexts(
            this IServiceCollection services, 
            IConfiguration configuration, 
            IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                RegisterProductionContexts(services, configuration);
            }
            else
            {
                RegisterDevelopmentContexts(services, configuration);
            }

            services.TryAddScoped<IdentityContext>();
            services.TryAddScoped<CustomerContext>();
            services.TryAddScoped<RestaurantContext>();
            services.TryAddScoped<CatalogContext>();
            services.TryAddScoped<OrderingContext>();

            services.Configure<CatalogDatabaseSettings>(
                configuration.GetSection(nameof(CatalogDatabaseSettings)));
            services.Configure<BasketDatabaseSettings>(
                configuration.GetSection(nameof(BasketDatabaseSettings)));
            services.Configure<ChatDatabaseSettings>(
                configuration.GetSection(nameof(ChatDatabaseSettings)));

            return services;
        }

        private static IServiceCollection RegisterDevelopmentContexts(
            IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());

            services.AddDbContext<CustomerContext>(options =>
               options
                   .UseSqlServer(configuration.GetConnectionString("CustomerConnection"),
                       x => x.UseNetTopologySuite())
                       .LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());

            services.AddDbContext<RestaurantContext>(options =>
               options
                   .UseSqlServer(configuration.GetConnectionString("RestaurantConnection"),
                       x => x.UseNetTopologySuite())
                       .LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());

            services.AddDbContext<CatalogContext>(options =>
               options
                   .UseSqlServer(configuration.GetConnectionString("CatalogConnection"),
                       x => x.UseNetTopologySuite())
                       .LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());

            services.AddDbContext<OrderingContext>(options =>
               options
                   .UseSqlServer(configuration.GetConnectionString("OrderingConnection"))
                       .LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());

            return services;
        }

        private static IServiceCollection RegisterProductionContexts(
            IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<CustomerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CustomerConnection"), 
                    x => x.UseNetTopologySuite()));

            services.AddDbContext<RestaurantContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RestaurantConnection"), 
                    x => x.UseNetTopologySuite()));

            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"), 
                    x => x.UseNetTopologySuite()));

            services.AddDbContext<OrderingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnection")));

            return services;
        }
    }
}
