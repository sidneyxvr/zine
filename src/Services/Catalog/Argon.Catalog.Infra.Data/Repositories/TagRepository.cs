using Argon.Catalog.Domain;
using Argon.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class TagRepository : ITagRepository, IRepository<Tag>
    {
        private readonly CatalogContext _context;

        public TagRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            await _context.Tags.AddAsync(tag, cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Tag>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Tags
                .Where(t => ids.Contains(t.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
