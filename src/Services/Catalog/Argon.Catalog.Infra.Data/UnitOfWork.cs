using Argon.Catalog.Domain;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogContext _context;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository; 
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ITagRepository _tagRepository;

        public UnitOfWork(
            CatalogContext context, 
            ISupplierRepository supplierRepository, 
            IServiceRepository serviceRepository, 
            ICategoryRepository categoryRepository, 
            IDepartmentRepository departmentRepository, 
            ISubCategoryRepository subCategoryRepository, 
            ITagRepository tagRepository)
        {
            _context = context;
            _supplierRepository = supplierRepository;
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
            _departmentRepository = departmentRepository;
            _subCategoryRepository = subCategoryRepository;
            _tagRepository = tagRepository;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IDepartmentRepository DepartmentRepository => _departmentRepository;
        public IServiceRepository ServiceRepository => _serviceRepository;
        public ISubCategoryRepository SubCategoryRepository => _subCategoryRepository;
        public ISupplierRepository SupplierRepository => _supplierRepository;
        public ITagRepository TagRepository => _tagRepository;

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            //TODO: Publish events

            return success;
        }
    }
}
