using Argon.Customers.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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

        public async Task AddAsync(Customer customer, 
            CancellationToken calcelationToken = default)
        {
            await _context.AddAsync(customer, calcelationToken);

            _context.Entry(customer).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Customer?> GetByIdAsync(
            Guid id,
            Include include = Include.None,
            CancellationToken calcelationToken = default)
        {
            return await _context.Customers
                .AsSplitQuery()
                .Includes(include)
                .FirstOrDefaultAsync(c => c.Id == id, calcelationToken);
        }

        public Task UpdateAsync(Customer customer, 
            CancellationToken calcelationToken = default)
        {
            //EF Core already tracks the entity
            //_context.Update(customer);

            return Task.CompletedTask;
        }
    }

    internal static class SupplierQueryExtentios
    {
        internal static IQueryable<Customer> Includes(this IQueryable<Customer> source, Include include)
        {
            if (include.HasFlag(Include.MainAddress))
            {
                source = source.Include(s => s.MainAddress);
            }

            if (include.HasFlag(Include.Addresses))
            {
                source = source.Include(s => s.Addresses);
            }

            return source;
        }
    }

    internal static class LinqEFExtentions
    {
        internal static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source, bool evaluation, Expression<Func<T, bool>> predicate)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return evaluation ? source.Where(predicate) : source;
        }

        public static IQueryable<T> OrderByIf<T>(
            this IQueryable<T> source, bool evaluation, Expression<Func<T, bool>> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return evaluation ? source.OrderBy(predicate) : source;
        }

        public static IQueryable<T> OrderByDescendingIf<T>(
            this IQueryable<T> source, bool evaluation, Expression<Func<T, bool>> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return evaluation ? source.OrderByDescending(predicate) : source;
        }
    }
}
