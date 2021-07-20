using Argon.Catalog.Domain;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogContext _context;
        private readonly IProductRepository _serviceRepository;
        private readonly IRestaurantRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository; 

        public UnitOfWork(
            CatalogContext context, 
            IProductRepository serviceRepository, 
            IRestaurantRepository supplierRepository, 
            ICategoryRepository categoryRepository)
        {
            _context = context;
            _serviceRepository = serviceRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ServiceRepository => _serviceRepository;
        public IRestaurantRepository RestaurantRepository => _supplierRepository;

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            //TODO: Publish events

            return success;
        }
    }
}
