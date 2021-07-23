using Argon.Catalog.QueryStack.Cache;
using Argon.Catalog.QueryStack.Models;
using Argon.Catalog.QueryStack.Response;
using Argon.Catalog.QueryStack.Services;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.QueryStack.Queries
{
    public class RestaurantQueries : IRestaurantQueries
    {
        private readonly IRestaurantCache _restaurantCache;
        private readonly IRestaurantService _restaurantService;

        public RestaurantQueries(
            IRestaurantCache restaurantCache, 
            IRestaurantService restaurantService)
            => (_restaurantCache, _restaurantService) = (restaurantCache, restaurantService);

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            var restaurant = await _restaurantCache.GetByIdAsync(id);

            if(restaurant is not null)
            {
                return restaurant;
            }

            restaurant = await _restaurantService.GetByIdAsync(id);

            if(restaurant is not null)
            {
                await _restaurantCache.AddAsync(restaurant!);
            }

            return restaurant;
        }

        public Task<RestaurantDetails?> GetRestaurantDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
