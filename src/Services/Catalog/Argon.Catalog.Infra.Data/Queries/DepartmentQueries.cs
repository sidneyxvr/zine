using Argon.Catalog.Application.Queries;
using Argon.Catalog.Application.Reponses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Queries
{
    public class DepartmentQueries : IDepartmentQueries
    {
        private readonly CatalogContext _context;

        public DepartmentQueries(CatalogContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<DepartmentResponse>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => d.IsActive)
                .Select(d => new DepartmentResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToArrayAsync();
        }
    }
}
