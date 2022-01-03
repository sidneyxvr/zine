using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace Argon.Zine.App.Api.Configurations;

public static class HealthChecksConfiguration
{
    public static IServiceCollection RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var timeout = TimeSpan.FromSeconds(1);

        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("CatalogConnection"), name: "Catalog Database", tags: new[] { "sql server" }, timeout: timeout)
            .AddSqlServer(configuration.GetConnectionString("CustomerConnection"), name: "Customer Database", tags: new[] { "sql server" }, timeout: timeout)
            .AddSqlServer(configuration.GetConnectionString("OrderingConnection"), name: "Ordering Database", tags: new[] { "sql server" }, timeout: timeout)
            .AddSqlServer(configuration.GetConnectionString("IdentityConnection"), name: "Identity Database", tags: new[] { "sql server" }, timeout: timeout)
            .AddSqlServer(configuration.GetConnectionString("RestaurantConnection"), name: "Restaurant Database", tags: new[] { "sql server" }, timeout: timeout)
            .AddMongoDb(mongodbConnectionString: configuration["BasketDatabaseSettings:ConnectionString"], name: "Basket Database", tags: new[] { "mongodb" }, timeout: timeout)
            .AddMongoDb(mongodbConnectionString: configuration["ChatDatabaseSettings:ConnectionString"], name: "Chat Database", tags: new[] { "mongodb" }, timeout: timeout)
            .AddRabbitMQ(provider => provider.GetRequiredService<IConnectionFactory>(), name: "Message Broker", tags: new[] { "rabbitmq" }, timeout: timeout)
            .AddRedis(configuration.GetConnectionString("CatalogRedis"), "Catalog Redis", tags: new[] { "redis" }, timeout: timeout)
            .AddEventStore(configuration.GetConnectionString("EventSourcingConnection"), name: "Event Sourcing Database", tags: new[] { "event store" }, timeout: timeout);
        
        services.AddHealthChecksUI().AddSqliteStorage("Data Source=healthchecks.db");

        return services;    
    }

    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).UseHealthChecksUI(options =>
        {
            options.UIPath = "/show-health-ui";
            options.ApiPath = "/health-ui-api";
        });

        return app;
    }
}