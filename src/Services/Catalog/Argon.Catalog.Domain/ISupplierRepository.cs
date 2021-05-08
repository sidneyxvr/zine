using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface ISupplierRepository
    {
        Task AddAsync(Supplier supplier);
    }
}
