using Argon.Catalog.QueryStack.Cache;
using Argon.Catalog.QueryStack.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Argon.Catalog.Caching
{
    public class RestaurantCache : IRestaurantCache
    {
        private readonly IDistributedCache _cache;

        public RestaurantCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task AddAsync(Restaurant restaurant)
            => await _cache.SetAsync(restaurant.Id.ToString(), 
                JsonSerializer.SerializeToUtf8Bytes(restaurant), 
                new DistributedCacheEntryOptions 
                { 
                    SlidingExpiration = TimeSpan.FromMinutes(15),
                });

        public async Task DeleteAsync(Guid id)
            => await _cache.RemoveAsync(id.ToString());

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            var cached = await _cache.GetAsync(id.ToString());
        
            if(cached is null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Restaurant?>(
                Encoding.UTF8.GetString(cached));
        }
    }
}
