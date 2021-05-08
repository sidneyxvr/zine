using Argon.Core.Data;
using System;
using System.Threading.Tasks;

namespace Argon.Customers.Domain
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task UpdateAsync(Address address);
        Task<Customer> GetByIdAsync(Guid id, params Include[] includes);
        Task<Address> GetAddressAsync(Guid customerId, Guid addressId);
        Task AddAsync(Address customer); 
    }

    public enum Include
    {
        Customer = 1,
        Addresses,
        MainAddress
    }
}
