using Argon.Core.Data;

namespace Argon.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
