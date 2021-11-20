using Argon.Zine.Restaurants.QueryStack.Reponses;

namespace Argon.Zine.Restaurants.QueryStack.Queries;

public interface IRestaurantQueries
{
    Task<RestaurantDetailsReponse?> GetRestaurantByUserIdAsync(Guid userId);
}