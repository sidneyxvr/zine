using Argon.Zine.Commom.Data;

namespace Argon.Zine.Customers.Domain;

public interface ICustomerRepository : IRepository<Customer>
{
    Task AddAsync(Customer customer, CancellationToken calcelationToken = default);
    Task AddAsync(Address address, CancellationToken calcelationToken = default);
    ValueTask UpdateAsync(Customer customer, CancellationToken calcelationToken = default);
    Task<Customer?> GetByIdAsync(Guid id, Includes includes = Includes.None,
        CancellationToken calcelationToken = default);
}

[Flags]
public enum Includes
{
    None = 0,
    MainAddress = 1,
    Addresses = 2,
    All = MainAddress | Addresses
}