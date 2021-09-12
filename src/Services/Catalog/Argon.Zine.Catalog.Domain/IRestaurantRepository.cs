using Argon.Zine.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Domain
{
    public interface IRestaurantRepository: IRepository<Restaurant>
    {
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task<Restaurant?> GetByIdAsync(Guid id);
    }
}
