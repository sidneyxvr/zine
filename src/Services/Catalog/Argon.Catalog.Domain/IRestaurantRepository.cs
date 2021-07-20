using Argon.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IRestaurantRepository: IRepository<Restaurant>
    {
        Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
