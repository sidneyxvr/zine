using Argon.Zine.Catalog.Domain;
using Argon.Zine.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Catalog.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository, IRepository<Category>
{
    private readonly CatalogContext _context;

    public CategoryRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(category, cancellationToken);
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.AsNoTracking().AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.AsNoTracking().AnyAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}