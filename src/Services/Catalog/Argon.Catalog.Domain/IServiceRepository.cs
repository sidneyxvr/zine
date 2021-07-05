using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IServiceRepository
    {
        Task AddAsync(Service service, CancellationToken cancellationToken = default);
    }
}
