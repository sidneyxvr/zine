using Argon.Catalog.Domain;
using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class ServiceRepository : IServiceRepository, IRepository<Service>
    {
        public Task AddAsync(Service service, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
