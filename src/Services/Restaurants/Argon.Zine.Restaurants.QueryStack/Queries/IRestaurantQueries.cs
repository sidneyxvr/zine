namespace Argon.Zine.Restaurants.QueryStack.Queries;

public interface IRestaurantQueries
{
    Task<Guid> GetRestaurantIdByUserIdAsync(Guid userId);
}