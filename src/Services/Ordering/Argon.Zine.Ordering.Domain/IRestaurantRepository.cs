using System;
using System.Threading.Tasks;

namespace Argon.Zine.Ordering.Domain
{
    public interface IRestaurantRepository : IDisposable
    {
        Task<Restaurant?> GetByIdAsync(Guid id);
    }
}
