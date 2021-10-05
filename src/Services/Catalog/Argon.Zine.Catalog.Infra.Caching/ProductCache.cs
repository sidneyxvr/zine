//using Argon.Zine.Catalog.QueryStack.Queries;
//using Argon.Zine.Catalog.QueryStack.Response;
//using Argon.Zine.Catalog.QueryStack.Services;
//using Microsoft.Extensions.Caching.Distributed;
//using System;
//using System.Threading.Tasks;

//namespace Argon.Zine.Catalog.Infra.Caching
//{
//    public class ProductCache : IProductQueries
//    {
//        private readonly IDistributedCache _cache;
//        private readonly IProductService _productService;

//        public Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<PagedList<ProductDetailsResponse>> GetProductsAsync()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
