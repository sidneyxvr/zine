using Argon.Basket.Requests;
using Argon.Basket.Services;
using Argon.Catalog.QueryStack.Queries;
using Microsoft.AspNetCore.Mvc;
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
                product.Price, request.Amount, product.ImageUrl, product!.RestaurantId, product.RestaurantName);

            var result = await _basketService.AddProductToBasket(productToBasket);

            return CustomResponse(result);
        }
    }
}
