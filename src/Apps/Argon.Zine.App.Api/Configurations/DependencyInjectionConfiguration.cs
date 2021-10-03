using Argon.Zine.App.Api.Extensions;
using Argon.Zine.Basket.Data;
using Argon.Zine.Basket.Models;
using Argon.Zine.Basket.Services;
using Argon.Zine.Core.Communication;
using Argon.Zine.Core.Data.EventSourcing;
using Argon.Zine.Core.DomainObjects;
using Argon.Zine.EventSourcing;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Argon.Zine.App.Api.Configurations
{
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
                var httpContext = provider.GetRequiredService<IHttpContextAccessor>();

                return new AppUser(httpContext);
            });

            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IBasketDAO, BasketDAO>();

            services.AddSingleton<IEventSourcingStorage, EventSourcingStorage>();
            services.AddSingleton<IEventStoreConnection>(provider => {
                var settings = ConnectionSettings.Create()
                    .DisableTls()
                    .UseDebugLogger()
                    .SetMaxDiscoverAttempts(1)
                    .EnableVerboseLogging();

                var connection = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@localhost:1113", settings);

                return connection;
            });

            BsonClassMap.RegisterClassMap<CustomerBasket>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(b => b.RestaurantLogoUrl)
                    .SetIgnoreIfNull(true);
                cm.MapField("_products")
                    .SetElementName("Products");
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("CatalogRedis");
            });

            return services;
        }
    }
}
