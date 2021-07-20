using Argon.Catalog.QueryStack.Models;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Cache
{
    public interface IRestaurantCache
    {
        Task<Restaurant?> GetByIdAsync(Guid id);
        Task AddAsync(Restaurant restaurant);
        Task DeleteAsync(Guid id);
    }
}
