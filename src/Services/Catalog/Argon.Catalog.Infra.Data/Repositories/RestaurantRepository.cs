using Argon.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly CatalogContext _context;

        public RestaurantRepository(CatalogContext context)
            => _context = context;

        public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
            => await _context.AddAsync(restaurant, cancellationToken);

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        public Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
