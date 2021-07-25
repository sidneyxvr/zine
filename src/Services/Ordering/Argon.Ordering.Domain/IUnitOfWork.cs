using System.Threading;
using System.Threading.Tasks;

namespace Argon.Ordering.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
        IOrderRepository OrderRepository { get; }
        IBuyerRepository BuyerRepository { get; }
    }
}
