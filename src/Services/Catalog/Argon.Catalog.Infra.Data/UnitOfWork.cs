using Argon.Catalog.Domain;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogContext _context;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository; 
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public UnitOfWork(
            CatalogContext context, 
            IServiceRepository serviceRepository, 
            ISupplierRepository supplierRepository, 
            ICategoryRepository categoryRepository, 
            IDepartmentRepository departmentRepository, 
            ISubCategoryRepository subCategoryRepository)
        {
            _context = context;
            _serviceRepository = serviceRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
            _departmentRepository = departmentRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IDepartmentRepository DepartmentRepository => _departmentRepository;
        public IServiceRepository ServiceRepository => _serviceRepository;
        public ISubCategoryRepository SubCategoryRepository => _subCategoryRepository;
        public ISupplierRepository SupplierRepository => _supplierRepository;

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            //TODO: Publish events

            return success;
        }
    }
}
