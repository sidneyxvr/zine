using Argon.Catalog.Domain;
using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository, IRepository<Product>
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
            => _context = context;
        

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
            => await _context.Products.AddAsync(product, cancellationToken);

        public void Dispose()
            => _context?.Dispose();
    }
}
