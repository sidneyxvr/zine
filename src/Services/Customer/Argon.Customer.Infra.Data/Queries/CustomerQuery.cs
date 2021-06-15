using Argon.Customers.QueryStack.Queries;
using Argon.Customers.QueryStack.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Customers.Infra.Data.Queries
{
    public class CustomerQuery : ICustomerQuery
    {
        private readonly CustomerContext _context;

        public CustomerQuery(CustomerContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }

        public Task<AddressResult?> GetAddressByCustomerId(Guid customerId, Guid addressId)
        {
            return _context.Addresses
                .Where(a => EF.Property<Guid>(a, "CustomerId") == customerId)
                .Select(a => new AddressResult
                {
                    City = a.City,
                    Complement = a.Complement,
                    Country = a.Country,
                    District = a.District,
                    Latitude = a.Location.Latitude,
                    Longitude = a.Location.Longitude,
                    Number = a.Number,
                    PostalCode = a.PostalCode,
                    State = a.State,
                    Street = a.Street
                })
                .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<AddressResult>> GetAddressesByCustomerId(Guid customerId)
        {
            var result = _context.Addresses
                .Where(a => EF.Property<Guid>(a, "CustomerId") == customerId)
                .Select(a => new AddressResult
                {
                    City = a.City,
                    Complement = a.Complement,
                    Country = a.Country,
                    District = a.District,
                    Latitude = a.Location.Latitude,
                    Longitude = a.Location.Longitude,
                    Number = a.Number,
                    PostalCode = a.PostalCode,
                    State = a.State,
                    Street = a.Street
                })
                .AsEnumerable();

            return Task.FromResult(result);
        }
    }
}
