using Argon.Catalog.QueryStack.Response;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Cache
{
    public interface IProductCache
    {
        Task<ProductDetailsResponse?> GetByIdAsync(Guid id);
        Task AddAsync(ProductDetailsResponse product);
        Task DeleteAsync(Guid id);
    }
}
