using Argon.Restaurants.Infra.Data;
using Argon.Zine.Restaurants.QueryStack.Queries;
using Argon.Zine.Restaurants.QueryStack.Reponses;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Restaurants.Infra.Data.Queries;

public class RestaurantQueries : IRestaurantQueries
{
    private readonly RestaurantContext _context;

    public RestaurantQueries(RestaurantContext context)
        => _context = context;

    public async Task<RestaurantDetailsReponse?> GetRestaurantByUserIdAsync(Guid userId)
        => await _context.Restaurants
        .Where(r => r.Users.Any(u => u.Id == userId))
        .ProjectToType<RestaurantDetailsReponse>()
        .FirstOrDefaultAsync();
}