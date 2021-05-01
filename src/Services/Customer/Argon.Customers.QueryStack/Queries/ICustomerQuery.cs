using Argon.Customers.QueryStack.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Customers.QueryStack.Queries
{
    public interface ICustomerQuery
    {
        Task<IEnumerable<AddressResult>> GetAddressesByCustomerId(Guid customerId);
        Task<AddressResult> GetAddressByCustomerId(Guid customerId, Guid addressId);
    }
}
