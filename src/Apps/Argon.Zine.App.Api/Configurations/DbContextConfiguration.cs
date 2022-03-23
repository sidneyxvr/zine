using Argon.Restaurants.Infra.Data;
using Argon.Zine.App.Api.Extensions;
using Argon.Zine.Basket.Data;
using Argon.Zine.Catalog.Infra.Data;
using Argon.Zine.Chat.Data;
using Argon.Zine.Customers.Infra.Data;
using Argon.Zine.Identity.Data;
using Argon.Zine.Ordering.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace Argon.Zine.App.Api.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection RegisterDbContexts(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var openTelemetrySettings = configuration
            .GetSection(nameof(OpenTelemetrySettings))
            .Get<OpenTelemetrySettings>();

        if (env.IsDevelopment())
        {
            RegisterDevelopmentContexts(services, configuration, openTelemetrySettings.Enable);
        }
        else
        {
            RegisterProductionContexts(services, configuration);
        }

        services.TryAddScoped<IdentityContext>();
        services.TryAddScoped<CustomerContext>();
        services.TryAddScoped<RestaurantContext>();
        services.TryAddScoped<CatalogContext>();
        services.TryAddScoped<OrderingContext>();

        services.Configure<BasketDatabaseSettings>(
            configuration.GetSection(nameof(BasketDatabaseSettings)));

        services.AddSingleton<BasketContext>(provider =>
        {
            var settings = configuration.GetSection(nameof(BasketDatabaseSettings))
                .Get<BasketDatabaseSettings>();

            var client = new MongoClient(
                GetMongoClientSettingsFromConnectionString(
                    settings.ConnectionString, env, openTelemetrySettings.Enable));
            var database = client.GetDatabase(settings.DatabaseName);

            return new BasketContext(database);
        });

        services.AddSingleton<ChatContext>(provider =>
        {
            var settings = configuration.GetSection(nameof(ChatDatabaseSettings))
                .Get<ChatDatabaseSettings>();

            var client = new MongoClient(
                GetMongoClientSettingsFromConnectionString(
                    settings.ConnectionString, env, openTelemetrySettings.Enable));
            var database = client.GetDatabase(settings.DatabaseName);

            return new ChatContext(database);
        });

        return services;
    }

    private static MongoClientSettings GetMongoClientSettingsFromConnectionString(
        string connectionString, IWebHostEnvironment env, bool openTelemetryIsEnabled)
    {
        var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
        if (!env.IsDevelopment())
        {
            return clientSettings;
        }

        clientSettings.ClusterConfigurator = options =>
        {
            if (openTelemetryIsEnabled)
            {
                options.Subscribe(new DiagnosticsActivityEventSubscriber(
                    new InstrumentationOptions { CaptureCommandText = true }));
            }
            else
            {
                options.Subscribe<CommandStartedEvent>(e 
                    => Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}"));
            }
        };

        return clientSettings;
    }

    private static IServiceCollection RegisterDevelopmentContexts(
        IServiceCollection services,
        IConfiguration configuration,
        bool openTelemetryIsEnable)
    {
        services.AddDbContext<IdentityContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"))
                .LogIfOpenTelemetryIsNotEnable(openTelemetryIsEnable));

        services.AddDbContext<CustomerContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CustomerConnection"),
                x => x.UseNetTopologySuite())
                .LogIfOpenTelemetryIsNotEnable(openTelemetryIsEnable));

        services.AddDbContext<RestaurantContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("RestaurantConnection"),
                x => x.UseNetTopologySuite())
                .LogIfOpenTelemetryIsNotEnable(openTelemetryIsEnable)
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning)));

        services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"),
                x => x.UseNetTopologySuite())
                .LogIfOpenTelemetryIsNotEnable(openTelemetryIsEnable));

        services.AddDbContext<OrderingContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("OrderingConnection"))
                .LogIfOpenTelemetryIsNotEnable(openTelemetryIsEnable));

        return services;
    }

    private static DbContextOptionsBuilder LogIfOpenTelemetryIsNotEnable(
        this DbContextOptionsBuilder dbContextOptionsBuilder, bool openTelemetryIsEnabled)
    {
        if (openTelemetryIsEnabled)
        {
            return dbContextOptionsBuilder;
        }

        return dbContextOptionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }

    private static IServiceCollection RegisterProductionContexts(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

        services.AddDbContext<CustomerContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CustomerConnection"),
                x => x.UseNetTopologySuite()));

        services.AddDbContext<RestaurantContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("RestaurantConnection"),
                x => x.UseNetTopologySuite()));

        services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"),
                x => x.UseNetTopologySuite()));

        services.AddDbContext<OrderingContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("OrderingConnection")));

        return services;
    }
}