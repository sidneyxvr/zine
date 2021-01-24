using Argon.Core.Data;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Argon.Customers.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Customer customer)
        {
            _context.Entry(customer).Property("CreatedAy").CurrentValue = DateTime.UtcNow;

            await _context.AddAsync(customer);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .Include(c => c.MainAddress)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            return Task.CompletedTask;
        }
    }
}
