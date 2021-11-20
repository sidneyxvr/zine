using Argon.Zine.Core.Data;

namespace Argon.Zine.Catalog.Domain;

public interface IRestaurantRepository : IRepository<Restaurant>
{
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task<Restaurant?> GetByIdAsync(Guid id);
}