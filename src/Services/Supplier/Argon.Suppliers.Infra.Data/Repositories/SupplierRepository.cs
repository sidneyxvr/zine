using Argon.Suppliers.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
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

        public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(supplier, cancellationToken);

            _context.Entry(supplier).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        public async Task<Supplier?> GetByIdAsync(
            Guid id, Include include = Include.None, CancellationToken cancellationToken = default)
        {
            return await _context.Suppliers
                .Includes(include)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    internal static class SupplierQueryExtentios
    {
        internal static IQueryable<Supplier> Includes(this IQueryable<Supplier> source, Include include)
        {
            if (include.HasFlag(Include.User))
            {
                source = source.Include(s => s.Users);
            }

            if (include.HasFlag(Include.Address))
            {
                source = source.Include(s => s.Users);
            }

            return source;
        }
    }
}
