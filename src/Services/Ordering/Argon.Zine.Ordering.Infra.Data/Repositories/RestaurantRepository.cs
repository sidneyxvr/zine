using Argon.Zine.Ordering.Domain;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Ordering.Infra.Data.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly OrderingContext _context;

    public RestaurantRepository(OrderingContext context)
        => _context = context;

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id)
        => await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
}