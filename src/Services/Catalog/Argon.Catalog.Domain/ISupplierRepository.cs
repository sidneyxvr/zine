using Argon.Core.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ISupplierRepository: IRepository<Supplier>
    {
        Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default);
    }
}
