using Argon.Zine.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Restaurants.Domain;

public interface IRestaurantRepository : IRepository<Restaurant>
{
    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task<Restaurant?> GetByIdAsync(
        Guid id, 
        Include include = Include.None, 
        CancellationToken cancellationToken = default);
}

[Flags]
public enum Include
{
    None = 0,
    Address = 1,
    User = 2,
    All = Address | User
}