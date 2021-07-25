using Argon.Customers.Application.Reponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Customers.Application.Queries
{
    public interface ICustomerQueries : IDisposable
    {
        Task<IEnumerable<AddressReponse>> GetAddressesByCustomerId(Guid customerId);
        Task<AddressReponse?> GetAddressByCustomerId(Guid customerId, Guid addressId);
    }
}
