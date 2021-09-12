using Argon.Zine.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Domain
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task AddAsync(Customer customer, CancellationToken calcelationToken = default);
        Task AddAsync(Address address, CancellationToken calcelationToken = default);
        Task UpdateAsync(Customer customer, CancellationToken calcelationToken = default);
        Task<Customer?> GetByIdAsync(Guid id, Include include = Include.None,
            CancellationToken calcelationToken = default);
    }

    [Flags]
    public enum Include
    {
        None = 0,
        MainAddress = 1,
        Addresses = 2,
        All = MainAddress | Addresses
    }
}
