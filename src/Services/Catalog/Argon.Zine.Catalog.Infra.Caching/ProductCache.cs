using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Response;

namespace Argon.Zine.Catalog.Infra.Caching
{
    public class ProductCache : IProductQueries
    {
        //public Task AddAsync(ProductDetailsResponse product)
        //    => Task.CompletedTask;

        //public Task DeleteAsync(Guid id)
        //    => Task.CompletedTask;

        //public Task<ProductDetailsResponse?> GetByIdAsync(Guid id)
        //    => Task.FromResult((ProductDetailsResponse?)null);
        public Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
