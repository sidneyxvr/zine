using Argon.Restaurants.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class RestaurantSetup
{
    public static IServiceProvider CreateRestaurantDatabase(this IServiceProvider services)
    {
        var restaurantContext = services.GetRequiredService<RestaurantContext>();
        restaurantContext.Database.EnsureDeleted();
        restaurantContext.Database.EnsureCreated();

        return services;
    }
}