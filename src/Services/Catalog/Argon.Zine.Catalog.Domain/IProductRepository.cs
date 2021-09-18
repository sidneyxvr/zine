using Argon.Zine.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
