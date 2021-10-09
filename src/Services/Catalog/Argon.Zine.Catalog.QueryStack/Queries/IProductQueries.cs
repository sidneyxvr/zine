using Argon.Zine.Catalog.QueryStack.Responses;
using Argon.Zine.Shared;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public interface IProductQueries
    {
        Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id);
        Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id);
        Task<PagedList<ProductItemGridResponse>> GetProductsAsync();
    }
}
