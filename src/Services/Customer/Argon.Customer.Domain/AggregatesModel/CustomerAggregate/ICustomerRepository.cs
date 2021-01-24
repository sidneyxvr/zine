using Argon.Core.Data;
using System;
using System.Threading.Tasks;

namespace Argon.Customers.Domain.AggregatesModel.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<Customer> GetByIdAsync(Guid id);
    }
}
