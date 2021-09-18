using Argon.Zine.Catalog.QueryStack.Response;
using Argon.Zine.Catalog.QueryStack.Services;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductService _productService;

        public ProductQueries(
            IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
            => await _productService.GetProductBasketByIdAsync(id);

        public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
            => await _productService.GetProductDetailsByIdAsync(id);
    }
}
