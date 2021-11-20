using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Responses;
using Argon.Zine.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Catalog.Infra.Data.Queries;

public class ProductQueries : IProductQueries
{
    private readonly CatalogContext _context;

    public ProductQueries(CatalogContext context)
        => _context = context;

    public async Task<ProductBasketResponse?> GetProductBasketByIdAsync(
        Guid id, CancellationToken cancellationToken)
        => await _context.Products
        .Where(p => p.Id == id)
        .ProjectToType<ProductBasketResponse>()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(
        Guid id, CancellationToken cancellationToken)
        => await _context.Products
        .Where(p => p.Id == id)
        .ProjectToType<ProductDetailsResponse>()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<PagedList<ProductItemGridResponse>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var query = _context.Products;

        var list = await query.ProjectToType<ProductItemGridResponse>()
            .ToListAsync(cancellationToken);
        var count = await query.CountAsync(cancellationToken);

        return new(list, count);
    }
}