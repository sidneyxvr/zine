using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Argon.Storage;
using Argon.Zine.App.Api.Extensions;
using Argon.Zine.Basket.Data;
using Argon.Zine.Basket.Models;
using Argon.Zine.Basket.Services;
using Argon.Zine.Commom.Communication;
using Argon.Zine.Commom.Data;
using Argon.Zine.Commom.Data.EventSourcing;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.EventSourcing;
using Argon.Zine.Storage;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;

namespace Argon.Zine.App.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //General
        services.AddMediatR(typeof(Startup).Assembly);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IBus, InMemoryBus>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        services.AddScoped<IAppUser>(provider =>
        {
            var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext!;

            var id = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var firstName = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.GivenName);
            var lastName = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.FamilyName);

            return new AppUser(Guid.Parse(id), firstName, lastName);
        });

        services.AddScoped<IBasketService, BasketService>();
        services.AddSingleton<IBasketDao, BasketDao>();

        services.AddSingleton<IEventSourcingStorage, EventSourcingStorage>();
        services.AddSingleton<IEventStoreConnection>(provider =>
        {
            var settings = ConnectionSettings.Create()
                .DisableTls()
                .UseDebugLogger()
                .SetMaxDiscoverAttempts(1)
#if DEBUG
                .EnableVerboseLogging()
#endif
                ;

            return EventStoreConnection.Create(configuration.GetConnectionString("EventSourcingConnection"), settings);
        });

        BsonClassMap.RegisterClassMap<CustomerBasket>(cm =>
        {
            var ctor = typeof(CustomerBasket).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, Type.EmptyTypes)!;

            var body = Expression.New(ctor);
            var lambda = Expression.Lambda(body);

            cm.AutoMap();
            cm.MapCreator(lambda.Compile());
            cm.UnmapMember(b => b.Total);
            cm.MapConstructor(ctor);
            cm.MapProperty(b => b.RestaurantLogoUrl)
                .SetIgnoreIfNull(true);
            cm.MapField("_products")
                .SetElementName("Products");
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("CatalogRedis");
            options.InstanceName = "catalog";
        });

        var s3SettingsSection = configuration.GetSection(nameof(S3Settings));
        services.Configure<S3Settings>(s3SettingsSection);

        var s3Settings = s3SettingsSection.Get<S3Settings>();

        services.AddScoped<AmazonS3Client>(provider
            => new AmazonS3Client(s3Settings.AccessId, s3Settings.AccessKey, RegionEndpoint.GetBySystemName(s3Settings.Region)));

        services.AddScoped<TransferUtility>(provider
            => new TransferUtility(provider.GetRequiredService<AmazonS3Client>()));

        services.TryAddScoped<IFileStorage>(provider
            => new FileStorage(s3Settings.BucketName, provider.GetRequiredService<TransferUtility>()));

        return services;
    }
}