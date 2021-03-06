using Argon.Restaurants.Domain;
using Argon.Zine.Commom.Communication;

namespace Argon.Restaurants.Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly IBus _bus;
    private readonly RestaurantContext _context;
    private readonly IRestaurantRepository _restaurantRepository;

    public UnitOfWork(IBus bus,
        RestaurantContext context,
        IRestaurantRepository restaurantRepository)
    {
        _bus = bus;
        _context = context;
        _restaurantRepository = restaurantRepository;
    }

    public IRestaurantRepository RestaurantRepository 
        => _restaurantRepository;

    public async Task<bool> CommitAsync()
    {
        var success = await _context.SaveChangesAsync() > 0;

        if (success) await _bus.PublishEventsAsync(_context);

        return true;
    }
}