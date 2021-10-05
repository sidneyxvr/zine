using Argon.Zine.Catalog.Shared.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public interface IRestaurantQueries
    {
        Task<RestaurantDetailsResponse?> GetRestaurantDetailsByIdAsync(Guid id);
    }
}
