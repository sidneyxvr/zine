using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Core.Communication;
using Argon.Zine.Core.DomainObjects;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V1;

[Route("api/products")]
[ApiController]
public class ProductsController : BaseController
{
    private readonly IBus _bus;
    private readonly IAppUser _appUser;
    private readonly IProductQueries _productQueries;
    private readonly ILogger<ProductsController> _logger;
    private readonly Restaurants.QueryStack.Queries.IRestaurantQueries _restaurantQueries;

    public ProductsController(
        IBus bus,
        IAppUser appUser,
        IProductQueries productQueries,
        ILogger<ProductsController> logger,
        Restaurants.QueryStack.Queries.IRestaurantQueries restaurantQueries)
    {
        _bus = bus;
        _logger = logger;
        _appUser = appUser;
        _productQueries = productQueries;
        _restaurantQueries = restaurantQueries;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromForm] CreateProductCommand command)
    {
        var restaurant = await _restaurantQueries.GetRestaurantByUserIdAsync(_appUser.Id);

        command.SetRestaurantId(restaurant!.Id);

        return CustomResponse(await _bus.SendAsync(command));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        => Ok(await _productQueries.GetProductDetailsByIdAsync(id, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetProducsAsync(CancellationToken cancellationToken)
        => Ok(await _productQueries.GetProductsAsync(cancellationToken));
}