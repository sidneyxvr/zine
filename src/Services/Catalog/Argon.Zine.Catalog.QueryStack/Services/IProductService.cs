using Argon.Zine.Catalog.QueryStack.Models;
using Argon.Zine.Catalog.QueryStack.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.QueryStack.Services
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id);
        Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id);
    }
}
