using Argon.Zine.Catalog.QueryStack.Models;
using Argon.Zine.Catalog.QueryStack.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public interface IRestaurantQueries
    {
        Task<Restaurant?> GetByIdAsync(Guid id);
        Task<RestaurantDetails?> GetRestaurantDetailsByIdAsync(Guid id);
    }
}
