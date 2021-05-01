using Argon.Core.Data;
using Argon.Core.DomainObjects;
using System;
using System.Threading.Tasks;

namespace Argon.Customers.Domain
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<Customer> GetByIdAsync(Guid id);
        Task AddAddressAsync(Address customer); 
    }
}
