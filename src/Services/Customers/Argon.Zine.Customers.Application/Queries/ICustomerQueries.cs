using Argon.Zine.Customers.Application.Reponses;

namespace Argon.Zine.Customers.Application.Queries
{
    public interface ICustomerQueries : IDisposable
    {
        Task<IEnumerable<AddressReponse>> GetAddressesByCustomerIdAsync(
            Guid customerId, CancellationToken cancellationToken = default);
        public Task<AddressReponse?> GetAddressAsync(
            Guid customerId, Guid addressId, CancellationToken cancellationToken = default);
        Task<CustomerNameResponse?> GetCustomerNameByIdAsync(Guid id);
    }
}
