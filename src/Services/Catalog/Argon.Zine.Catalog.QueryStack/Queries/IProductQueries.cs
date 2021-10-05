using Argon.Zine.Catalog.Shared.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public interface IProductQueries
    {
        Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id);
        Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id);
    }
}
