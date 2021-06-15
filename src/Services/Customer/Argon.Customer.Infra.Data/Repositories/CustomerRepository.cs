using Argon.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public async Task AddAsync(Customer customer)
        {
            await _context.AddAsync(customer);

            _context.Entry(customer).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        public async Task AddAsync(Address customer)
        {
            await _context.Addresses.AddAsync(customer);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Address?> GetAddressAsync(Guid customerId, Guid addressId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.CustomerId == customerId && a.Id == addressId);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, params Include[] includes)
        {
            var query = _context.Customers.AsQueryable();

            if (includes.Contains(Include.Addresses))
            {
                query = query.Include(c => c.Addresses);
            }

            if (includes.Contains(Include.MainAddress))
            {
                //query = query.Include(c => c.MainAddress);
            }

            return await query.AsSplitQuery()
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateAsync(Customer customer)
        {
            //EF Core already tracks the enetity
            //_context.Update(customer);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Address address)
        {
            //EF Core already tracks the enetity
            //_context.Update(customer);

            return Task.CompletedTask;
        }
    }
}
