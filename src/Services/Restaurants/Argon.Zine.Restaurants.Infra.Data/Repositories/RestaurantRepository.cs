using Argon.Restaurants.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Restaurants.Infra.Data.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantContext _context;

    public RestaurantRepository(RestaurantContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(restaurant, cancellationToken);

        _context.Entry(restaurant).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        foreach (var user in restaurant.Users)
        {
            _context.Entry(user).Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Restaurant?> GetByIdAsync(
        Guid id, Include include = Include.None, CancellationToken cancellationToken = default)
        => await _context.Restaurants
            .Includes(include)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}

internal static class SupplierQueryExtentios
{
    internal static IQueryable<Restaurant> Includes(this IQueryable<Restaurant> source, Include include)
    {
        if (include.HasFlag(Include.User))
        {
            source = source.Include(s => s.Users);
        }

        if (include.HasFlag(Include.Address))
        {
            source = source.Include(s => s.Users);
        }

        return source;
    }
}