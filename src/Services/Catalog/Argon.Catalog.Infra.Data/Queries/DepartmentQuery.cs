using Argon.Catalog.QueryStack.Queries;
using Argon.Catalog.QueryStack.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Queries
{
    public class DepartmentQuery : IDepartmentQuery
    {
        private readonly CatalogContext _context;

        public DepartmentQuery(CatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentResult>> GetAllAsync()
        {
            return await _context.Departments
                .Where(d => d.IsActive)
                .Select(d => new DepartmentResult {
                   Id = d.Id,
                   Name = d.Name,
                   Description = d.Description
                })
                .ToArrayAsync();
        }
    }
}
