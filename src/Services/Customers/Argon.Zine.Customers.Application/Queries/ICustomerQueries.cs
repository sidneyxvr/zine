using Argon.Customers.Application.Reponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Customers.Application.Queries
{
    public interface ICustomerQueries : IDisposable
    {
        Task<IEnumerable<AddressReponse>> GetAddressesByCustomerIdAsync(Guid customerId);
        Task<AddressReponse?> GetAddressByCustomerIdAsync(Guid customerId, Guid addressId);
        Task<CustomerNameResponse?> GetCustomerNameByIdAsync(Guid id);
    }
}
