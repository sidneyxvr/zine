using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Responses;
using Argon.Zine.Commom.Communication;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Shared;
using Microsoft.AspNetCore.Mvc;
using IRestaurantQueries = Argon.Zine.Restaurants.QueryStack.Queries.IRestaurantQueries;
namespace Argon.Zine.App.Api.Controllers.V1;

[Route("api/products")]
[ApiController]
public class ProductsController : BaseController
{
    private readonly IBus _bus;
    private readonly IAppUser _appUser;
    private readonly IProductQueries _productQueries;
    private readonly IRestaurantQueries _restaurantQueries;

    public ProductsController(
        IBus bus,
        IAppUser appUser,
        IProductQueries productQueries,
        IRestaurantQueries restaurantQueries)
    {
        _bus = bus;
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
    public async Task<ProductDetailsResponse?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _productQueries.GetProductDetailsByIdAsync(id, cancellationToken);

    [HttpGet]
    public async Task<PagedList<ProductItemGridResponse>> GetProducsAsync(CancellationToken cancellationToken)
        => await _productQueries.GetProductsAsync(cancellationToken);
}