using Argon.Catalog.Application.Queries;
using Argon.Catalog.Application.Reponses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Queries
{
    public class TagQueries : ITagQueries
    {
        private readonly CatalogContext _context;

        public TagQueries(CatalogContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            return await _context.Tags
                .AsNoTracking()
                .Where(t => t.IsActive)
                .Select(t => new TagResponse
                {
                    Id = t.Id, 
                    Name = t.Name,
                })
                .ToListAsync();
        }
    }
}
