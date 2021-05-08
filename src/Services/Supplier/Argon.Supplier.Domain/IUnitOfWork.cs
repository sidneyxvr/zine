using System.Threading.Tasks;

namespace Argon.Suppliers.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        public ISupplierRepository SupplierRepository { get; }
    }
}
