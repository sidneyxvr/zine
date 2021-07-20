using Argon.Catalog.Domain;
using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository, IRepository<Product>
    {
        public Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
