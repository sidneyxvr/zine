using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task AddAsync(Service service, CancellationToken cancellationToken = default);
    }
}
