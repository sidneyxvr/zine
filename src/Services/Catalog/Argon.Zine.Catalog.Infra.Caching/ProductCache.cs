using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Responses;
using Argon.Zine.Shared;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Infra.Caching
{
    public class ProductCache : IProductQueries
    {
        private readonly IDistributedCache _cache;
        private readonly IProductQueries _productQueries;

        public ProductCache(IDistributedCache cache, IProductQueries productQueries)
        {
            _cache = cache;
            _productQueries = productQueries;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
            => _productQueries.GetProductBasketByIdAsync(id);

        public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
        {
            var productCached = await _cache.GetAsync(id.ToString());

            if(productCached is not null)
            {
                return JsonSerializer.Deserialize<ProductDetailsResponse?>(
                    Encoding.UTF8.GetString(productCached));
            }

            var product = await _productQueries.GetProductDetailsByIdAsync(id);

            if(product is not null)
            {
                await _cache.SetAsync(id.ToString(),
                    JsonSerializer.SerializeToUtf8Bytes(product),
                    new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(15),
                    });
            }

            return product;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<PagedList<ProductItemGridResponse>> GetProductsAsync()
            => _productQueries.GetProductsAsync();
    }
}
