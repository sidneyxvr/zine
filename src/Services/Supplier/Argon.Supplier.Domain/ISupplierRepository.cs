using Argon.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Suppliers.Domain
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task<Supplier?> GetByIdAsync(
            Guid id, Include include = Include.None, CancellationToken cancellationToken = default);
    }

    [Flags]
    public enum Include
    {
        None = 0,
        Address = 1,
        User = 2,
        All = Address | User
    }
}
