using System.Threading;
using System.Threading.Tasks;

namespace Argon.Ordering.Domain
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
    }
}
