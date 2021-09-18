using Argon.Zine.Catalog.QueryStack.Models;
using Argon.Zine.Catalog.QueryStack.Response;
using Argon.Zine.Catalog.QueryStack.Services;
using System;
using System.Threading.Tasks;

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

        public Task<RestaurantDetails?> GetRestaurantDetailsByIdAsync(Guid id)
            => throw new NotImplementedException();
    }
}
