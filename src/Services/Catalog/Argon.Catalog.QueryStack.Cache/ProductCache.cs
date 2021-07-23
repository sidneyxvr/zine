using Argon.Catalog.QueryStack.Cache;
using Argon.Catalog.QueryStack.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.Caching
{
    public class ProductCache : IProductCache
    {
        public Task AddAsync(ProductDetailsResponse product)
            => Task.CompletedTask;

        public Task DeleteAsync(Guid id)
            => Task.CompletedTask;

        public Task<ProductDetailsResponse?> GetByIdAsync(Guid id)
            => Task.FromResult((ProductDetailsResponse?)null);
    }
}
