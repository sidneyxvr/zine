using Argon.Zine.Core.Data;

namespace Argon.Zine.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
