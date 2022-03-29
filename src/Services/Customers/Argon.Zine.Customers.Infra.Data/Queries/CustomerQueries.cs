using Argon.Zine.Customers.Application.Queries;
using Argon.Zine.Customers.Application.Reponses;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Customers.Infra.Data.Queries;
public class CustomerQueries : ICustomerQueries
{
    private readonly CustomerContext _context;

    public CustomerQueries(CustomerContext context)
        => _context = context;

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task<AddressReponse?> GetAddressAsync(
        Guid customerId, Guid addressId, CancellationToken cancellationToken = default)
    {
        return _context.Addresses
            .Where(a => a.CustomerId == customerId && a.Id == addressId)
            .Select(a => new AddressReponse(a.Street, a.Number, a.District, a.City, a.State, 
                a.Country, a.PostalCode, a.Complement, a.Location.Latitude, a.Location.Longitude))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AddressReponse>> GetAddressesByCustomerIdAsync(
        Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Addresses
            .AsNoTracking()
            .Where(a => a.CustomerId == customerId)
            .Select(a => new AddressReponse(a.Street, a.Number, a.District, a.City, a.State, 
                a.Country, a.PostalCode, a.Complement, a.Location.Latitude, a.Location.Longitude))
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerNameResponse?> GetCustomerNameByIdAsync(Guid id)
        => await _context.Customers
        .AsNoTracking()
        .Select(c => new CustomerNameResponse(c.Id, c.Name.FirstName, c.Name.Surname))
        .FirstOrDefaultAsync(c => c.Id == id);
}