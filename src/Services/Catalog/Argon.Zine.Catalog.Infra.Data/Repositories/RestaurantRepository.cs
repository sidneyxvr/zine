using Argon.Zine.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Infra.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly CatalogContext _context;

        public RestaurantRepository(CatalogContext context)
            => _context = context;

        public async Task AddAsync(Restaurant restaurant)
            => await _context.AddAsync(restaurant);

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id)
            => await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

        public Task UpdateAsync(Restaurant restaurant)
            => Task.CompletedTask;
    }
}
