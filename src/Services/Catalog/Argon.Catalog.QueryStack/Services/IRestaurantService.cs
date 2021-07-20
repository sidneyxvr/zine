using Argon.Catalog.QueryStack.Models;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Services
{
    public interface IRestaurantService
    {
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task<Restaurant?> GetByIdAsync(Guid id);
    }
}
