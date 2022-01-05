using Argon.Zine.Customers.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Customers.Infra.Data.Repositories;

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

    public async Task AddAsync(Address address,
        CancellationToken calcelationToken = default)
    {
        await _context.AddAsync(address, calcelationToken);
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Customer?> GetByIdAsync(
        Guid id,
        Includes include = Includes.None,
        CancellationToken calcelationToken = default)
    {
        return await _context.Customers
            .AsSplitQuery()
            .Includes(include)
            .FirstOrDefaultAsync(c => c.Id == id, calcelationToken);
    }

    public ValueTask UpdateAsync(Customer customer,
        CancellationToken calcelationToken = default)
    {
        //EF Core already tracks the entity
        //_context.Update(customer);

        return ValueTask.CompletedTask;
    }
}

internal static class CustomerQueryExtentions
{
    internal static IQueryable<Customer> Includes(this IQueryable<Customer> source, Includes include)
    {
        if (include.HasFlag(Domain.Includes.MainAddress))
        {
            source = source.Include(s => s.MainAddress);
        }

        if (include.HasFlag(Domain.Includes.Addresses))
        {
            source = source.Include(s => s.Addresses);
        }

        return source;
    }
}