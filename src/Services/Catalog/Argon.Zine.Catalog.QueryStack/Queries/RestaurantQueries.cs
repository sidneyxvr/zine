using Argon.Zine.Catalog.QueryStack.Models;
using Argon.Zine.Catalog.QueryStack.Response;
using Argon.Zine.Catalog.QueryStack.Services;

namespace Argon.Zine.Catalog.QueryStack.Queries
{
    public class RestaurantQueries : IRestaurantQueries
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantQueries(
            IRestaurantService restaurantService)
            => _restaurantService = restaurantService;

        public async Task<Restaurant?> GetByIdAsync(Guid id)
            => await _restaurantService.GetByIdAsync(id);
            //var restaurant = await _restaurantCache.GetByIdAsync(id);

            //if(restaurant is not null)
            //{
            //    return restaurant;
            //}


            //if(restaurant is not null)
            //{
            //    await _restaurantCache.AddAsync(restaurant!);
            //}

            //return restaurant;

        public Task<RestaurantDetails?> GetRestaurantDetailsByIdAsync(Guid id)
            => throw new NotImplementedException();
    }
}
