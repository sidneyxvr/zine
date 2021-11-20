using Argon.Zine.Catalog.QueryStack.Responses;
using Argon.Zine.Shared;

namespace Argon.Zine.Catalog.QueryStack.Queries;

public interface IProductQueries
{
    Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedList<ProductItemGridResponse>> GetProductsAsync(CancellationToken cancellationToken = default);
}