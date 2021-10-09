using Argon.Zine.Catalog.QueryStack.Responses;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public interface IRestaurantQueries
    {
        Task<RestaurantDetailsResponse?> GetRestaurantDetailsByIdAsync(Guid id);
    }
}
