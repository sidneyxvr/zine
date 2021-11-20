using Argon.Restaurants.Application.Commands;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Core.Communication;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V1;

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
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => Ok(await _restaurantQueries.GetRestaurantDetailsByIdAsync(id, cancellationToken));

    [HttpPut("address")]
    public async Task<IActionResult> UpdateAddressAsync(UpdateAddressCommand command)
        => CustomResponse(await _bus.SendAsync(command));

    [HttpPut("open")]
    public async Task<IActionResult> OpenRestaurantAsync(OpenRestaurantCommand command)
        => CustomResponse(await _bus.SendAsync(command));

    [HttpPut("close")]
    public async Task<IActionResult> CloseRestaurantAsync(CloseRestaurantCommand command)
        => CustomResponse(await _bus.SendAsync(command));
}