using Argon.Zine.Catalog.Domain;
using Argon.Zine.Core.Communication;

namespace Argon.Zine.Catalog.Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly IBus _bus;
    private readonly CatalogContext _context;
    private readonly IProductRepository _productRepository;
    private readonly IRestaurantRepository _supplierRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UnitOfWork(
        IBus bus,
        CatalogContext context,
        IProductRepository productRepository,
        IRestaurantRepository supplierRepository,
        ICategoryRepository categoryRepository)
    {
        _bus = bus;
        _context = context;
        _productRepository = productRepository;
        _supplierRepository = supplierRepository;
        _categoryRepository = categoryRepository;
    }

    public ICategoryRepository CategoryRepository
        => _categoryRepository;
    public IProductRepository ProductRepository
        => _productRepository;
    public IRestaurantRepository RestaurantRepository
        => _supplierRepository;

    public async Task<bool> CommitAsync()
    {
        var success = await _context.SaveChangesAsync() > 0;

        if (success) await _bus.PublishAllAsync(_context);

        return success;
    }
}