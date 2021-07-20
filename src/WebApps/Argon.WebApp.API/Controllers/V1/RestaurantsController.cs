using Argon.Catalog.QueryStack.Queries;
using Argon.Core.Communication;
using Argon.Restaurants.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : BaseController
    {
        private readonly IBus _bus;
        private readonly IRestaurantQueries _restaurantQueries;

        public RestaurantsController(
            IBus bus, 
            IRestaurantQueries restaurantQueries)
        {
            _bus = bus;
            _restaurantQueries = restaurantQueries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Ok(await _restaurantQueries.GetByIdAsync(id));

        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddressAsync(UpdateAddressCommand command)
            => CustomResponse(await _bus.SendAsync(command));


        [HttpPut("open")]
        public async Task<IActionResult> UpdateAddressAsync(OpenRestaurantCommand command)
            => CustomResponse(await _bus.SendAsync(command));
    }
}