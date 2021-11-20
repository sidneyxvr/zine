using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Responses;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Argon.Zine.Catalog.Infra.Data.Queries;

public class RestaurantQueries : IRestaurantQueries
{
    private readonly CatalogContext _context;

    public RestaurantQueries(CatalogContext context)
        => _context = context;

    public async Task<RestaurantDetailsResponse?> GetRestaurantDetailsByIdAsync(
        Guid id, CancellationToken cancellationToken)
        => await _context.Restaurants
        .ProjectToType<RestaurantDetailsResponse>()
        .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
}