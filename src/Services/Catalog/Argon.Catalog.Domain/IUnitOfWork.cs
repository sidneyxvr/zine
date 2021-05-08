using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        public ISupplierRepository SupplierRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
    }
}
