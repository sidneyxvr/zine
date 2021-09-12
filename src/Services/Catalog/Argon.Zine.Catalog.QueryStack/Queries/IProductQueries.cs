using Argon.Zine.Catalog.QueryStack.Response;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public interface IProductQueries
    {
        Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id);
        Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id);
    }
}
