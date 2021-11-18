using Argon.Zine.Core.Data;

namespace Argon.Restaurants.Domain;

public interface IRestaurantRepository : IRepository<Restaurant>
{
    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task<Restaurant?> GetByIdAsync(
        Guid id, 
        Includes include = Includes.None, 
        CancellationToken cancellationToken = default);
}

[Flags]
public enum Includes
{
    None = 0,
    Address = 1,
    User = 2,
    All = Address | User
}