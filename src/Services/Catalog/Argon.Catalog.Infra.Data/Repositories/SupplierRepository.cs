using Argon.Catalog.Domain;
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

        public async Task AddAsync(Supplier supplier)
        {
            await _context.AddAsync(supplier);
        }
    }
}
