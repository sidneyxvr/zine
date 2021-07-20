using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
    }
}
