using Argon.Suppliers.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Suppliers.Infra.Data.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly SupplierContext _context;

        public SupplierRepository(SupplierContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Supplier supplier)
        {
            await _context.AddAsync(supplier);

            _context.Entry(supplier).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        public async Task<Address> GetAddressAsync(Guid supplierId, Guid addressId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == addressId && a.SupplierId == supplierId);
        }

        public async Task<Supplier> GetByIdAsync(Guid id, params Include[] includes)
        {
            if (includes.Length == 0)
            {
                return await _context.Suppliers.FindAsync(id);
            }

            var query = _context.Suppliers.AsQueryable();

            if (includes.Contains(Include.Address))
            {
                query = query.Include(s => s.Address);
            }

            if (includes.Contains(Include.Users))
            {
                query = query.Include(s => s.Users)
                    .AsSplitQuery();
            }

            return await query
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task UpdateAsync(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
