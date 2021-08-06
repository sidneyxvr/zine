using System;
using System.Threading.Tasks;

namespace Argon.Ordering.Domain
{
    public interface IRestaurantRepository : IDisposable
    {
        Task<Restaurant?> GetByIdAsync(Guid id);
    }
}
