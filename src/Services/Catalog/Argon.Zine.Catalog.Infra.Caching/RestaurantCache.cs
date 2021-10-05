//using Argon.Zine.Catalog.QueryStack.Models;
//using Argon.Zine.Catalog.QueryStack.Queries;
//using Argon.Zine.Catalog.QueryStack.Services;
//using Argon.Zine.Catalog.Shared.Response;
//using Microsoft.Extensions.Caching.Distributed;
//using System;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Argon.Zine.Catalog.Infra.Caching
//{
//    public class RestaurantCache : IRestaurantQueries
//    {
//        private readonly IDistributedCache _cache;

//        public RestaurantCache(IDistributedCache cache)
//        {
//            _cache = cache;
//        }

//        public async Task<Restaurant?> GetByIdAsync(Guid id)
//        {
//            var restaurantCached = await _cache.GetAsync(id.ToString());

//            if (restaurantCached is not null)
//            {
//                return JsonSerializer.Deserialize<Restaurant?>(
//                    Encoding.UTF8.GetString(restaurantCached));
//            }

//            var restaurant = await _restaurantService.GetByIdAsync(id);

//            if (restaurant is not null)
//            {
//                await _cache.SetAsync(id.ToString(),
//                    JsonSerializer.SerializeToUtf8Bytes(restaurant),
//                    new DistributedCacheEntryOptions
//                    {
//                        SlidingExpiration = TimeSpan.FromMinutes(15),
//                    });
//            }

//            return restaurant;
//        }

//        public async Task<RestaurantDetails?> GetRestaurantDetailsByIdAsync(Guid id)
//            => Map(await GetByIdAsync(id));

//        private static RestaurantDetails? Map(Restaurant? restaurant)
//            => restaurant is null 
//            ? null
//            : new() 
//            { 
//                Id = restaurant.Id,
//                LogoUrl = restaurant.LogoUrl,
//                Name = restaurant.Name,
//            };
//    }
//}
