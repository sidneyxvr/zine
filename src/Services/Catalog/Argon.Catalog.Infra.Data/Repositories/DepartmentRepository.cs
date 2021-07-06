using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CatalogContext _context;

        public DepartmentRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(department, cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.AnyAsync(d => d.Name == name, cancellationToken);
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }
    }
}
