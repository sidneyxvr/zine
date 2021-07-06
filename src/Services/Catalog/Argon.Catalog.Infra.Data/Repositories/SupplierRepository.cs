using Argon.Catalog.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly CatalogContext _context;

        public SupplierRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(supplier, cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
