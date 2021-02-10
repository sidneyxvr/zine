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
            await _context.AddAsync(customer);

            _context.Entry(customer).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Address> GetAddressAsync(Guid customerId, Guid addressId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => EF.Property<Guid>(a, "CustomerId") == customerId && a.Id == addressId);
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .Include(c => c.MainAddress)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            return Task.CompletedTask;
        }
    }
}
