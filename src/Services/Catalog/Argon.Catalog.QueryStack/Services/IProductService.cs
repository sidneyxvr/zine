using Argon.Catalog.QueryStack.Models;
using Argon.Catalog.QueryStack.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Services
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id);
        Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id);
    }
}
