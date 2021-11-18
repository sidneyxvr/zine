using Argon.Zine.Catalog.Domain;
using Argon.Zine.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Catalog.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository, IRepository<Product>
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
            => _context = context;
        

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
            => await _context.Products.AddAsync(product, cancellationToken);

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
