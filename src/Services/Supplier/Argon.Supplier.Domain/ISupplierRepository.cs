using System;
using System.Threading.Tasks;

namespace Argon.Suppliers.Domain
{
    public interface ISupplierRepository
    {
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Address address);
        Task<Address> GetAddressAsync(Guid supplierId, Guid addressId);
        Task<Supplier> GetByIdAsync(Guid id, params Include[] includes);
    }

    public enum Include
    {
        Supplier = 1,
        Address,
        Users
    }
}
