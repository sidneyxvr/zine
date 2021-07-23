using Argon.Catalog.QueryStack.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Queries
{
    public interface IProductQueries
    {
        Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id);
        Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id);
    }
}
