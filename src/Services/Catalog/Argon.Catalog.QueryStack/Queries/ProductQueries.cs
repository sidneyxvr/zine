using Argon.Catalog.QueryStack.Cache;
using Argon.Catalog.QueryStack.Response;
using Argon.Catalog.QueryStack.Services;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductCache _productCache;
        private readonly IProductService _productService;

        public ProductQueries(
            IProductCache productCache,
            IProductService productService)
        {
            _productCache = productCache;
            _productService = productService;
        }

        public async Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
            => await _productService.GetProductBasketByIdAsync(id);

        public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
        {
            var product = await _productCache.GetByIdAsync(id);

            if (product is not null)
            {
                return product;
            }

            product = await _productService.GetProductDetailsByIdAsync(id);

            if(product is not null)
            {
                await _productCache.AddAsync(product);
            }

            return product;
        }
    }
}
