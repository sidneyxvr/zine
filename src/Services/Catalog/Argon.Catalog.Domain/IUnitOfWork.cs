using System.Threading.Tasks;

namespace Argon.Catalog.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        public IServiceRepository ServiceRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public ISubCategoryRepository SubCategoryRepository { get; }
    }
}
