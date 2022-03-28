using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Services;
using Argon.Zine.Catalog.QueryStack.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V1;

[Route("api/basket")]
[ApiController]
[Authorize]
public class BasketController : BaseController
{
    private readonly IBasketService _basketService;
    private readonly IProductQueries _productQueries;

    public BasketController(
        IBasketService basketService,
        IProductQueries productQueries)
    {
        _basketService = basketService;
        _productQueries = productQueries;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToBasketAsync(ProductToBasketRequest request)
    {
        var product = await _productQueries.GetProductBasketByIdAsync(request.ProductId);

        var productToBasket = new ProductToBasketDto(product!.Id, product.Name,
            product.Price, request.Amount, product.ImageUrl, product!.RestaurantId,
            product.RestaurantName, product.RestaurantLogoUrl);

        await _basketService.AddProductToBasketAsync(productToBasket);

        return Ok();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveProductFromBasketAsync(Guid productId)
    {
        await _basketService.RemoveProductFromBasketAsync(productId);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketAsync(CancellationToken cancellationToken)
        => Ok(await _basketService.GetBasketAsync(cancellationToken));
}