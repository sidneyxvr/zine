using Argon.Basket.Requests;
using Argon.Basket.Services;
using Argon.Catalog.QueryStack.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/basket")]
    [ApiController]
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
            var product = await _productQueries.GetProductBasketByIdAsync(request.Id);

            var productToBasket = new ProductToBasketDTO(product!.Id, product.Name, 
                product.Price, request.Amount, product.ImageUrl, product!.RestaurantId, 
                product.RestaurantName, product.RestaurantLogoUrl);

            await _basketService.AddProductToBasket(productToBasket);

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProductFromBasketAsync(Guid productId)
        {
            await _basketService.RemoveProductFromBasket(productId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync()
            => Ok(await _basketService.GetBasketAsync());
    }
}
