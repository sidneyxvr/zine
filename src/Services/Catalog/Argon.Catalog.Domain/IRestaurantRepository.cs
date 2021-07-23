using Argon.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IRestaurantRepository: IRepository<Restaurant>
    {
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task<Restaurant?> GetByIdAsync(Guid id);
    }
}
