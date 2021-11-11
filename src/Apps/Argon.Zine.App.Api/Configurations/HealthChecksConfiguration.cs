using Argon.Restaurants.Infra.Data;
using Argon.Zine.Catalog.Infra.Data;
using Argon.Zine.Customers.Infra.Data;
using Argon.Zine.Identity.Data;
using Argon.Zine.Ordering.Infra.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Argon.Zine.App.Api.Configurations
{
    public static class HealthChecksConfiguration
    {
        public static IServiceCollection RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<CatalogContext>("Catalog Database", tags: new[] { "sql server" })
                .AddDbContextCheck<CustomerContext>("Customer Database", tags: new[] { "sql server" })
                .AddDbContextCheck<OrderingContext>("Ordering Database", tags: new[] { "sql server" })
                .AddDbContextCheck<IdentityContext>("Identity Database", tags: new[] { "sql server" })
                .AddDbContextCheck<RestaurantContext>("Restaurant Database", tags: new[] { "sql server" })
                .AddMongoDb(mongodbConnectionString: configuration["BasketDatabaseSettings:ConnectionString"], name: "Basket Database", tags: new[] { "mongodb" })
                .AddMongoDb(mongodbConnectionString: configuration["ChatDatabaseSettings:ConnectionString"], name: "Chat Database", tags: new[] { "mongodb" })
                .AddRabbitMQ(provider => provider.GetRequiredService<IConnectionFactory>(), name: "Message Broker", tags: new[] { "rabbitmq" })
                .AddRedis(configuration.GetConnectionString("CatalogRedis"), "Catalog Redis", tags: new[] { "redis" })
                .AddEventStore(configuration.GetConnectionString("EventSourcingConnection"), name: "Event Sourcing Database", tags: new[] { "event store" });

            services.AddHealthChecksUI().AddSqliteStorage("Data Source=healthchecks.db");

            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();

            return app;
        }
    }
}
