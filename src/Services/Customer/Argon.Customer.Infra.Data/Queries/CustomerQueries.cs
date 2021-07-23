﻿using Argon.Customers.Application.Queries;
using Argon.Customers.Application.Reponses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Customers.Infra.Data.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly CustomerContext _context;

        public CustomerQueries(CustomerContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<AddressReponse?> GetAddressByCustomerId(Guid customerId, Guid addressId)
        {
            return _context.Addresses
                .Where(a => a.CustomerId == customerId)
                .Select(a => new AddressReponse
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

        public async Task<IEnumerable<AddressReponse>> GetAddressesByCustomerId(Guid customerId)
        {
            return await _context.Addresses
                .AsNoTracking()
                .Where(a => a.CustomerId == customerId)
                .Select(a => new AddressReponse
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
                .ToListAsync();
        }
    }
}