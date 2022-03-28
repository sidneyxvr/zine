using Argon.Zine.Basket.Data;
using Argon.Zine.Basket.Models;
using Argon.Zine.Basket.Services;
using MongoDB.Bson.Serialization;
using System.Linq.Expressions;
using System.Reflection;

namespace Argon.Zine.App.Api.Configurations;
public static class BasketConfiguration
{
    public static IServiceCollection RegisterBasket(this IServiceCollection services)
    {

        services.AddScoped<IBasketService, BasketService>();
        services.AddSingleton<IBasketDao, BasketDao>();

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

        return services;
    }
}