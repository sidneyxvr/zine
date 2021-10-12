using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Core.Communication;
using Argon.Zine.Core.DomainObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.App.Api.Controllers.V1
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IBus _bus;
        private readonly IAppUser _appUser;
        private readonly IProductQueries _productQueries;
        private readonly Restaurants.QueryStack.Queries.IRestaurantQueries _restaurantQueries;

        public ProductsController(
            IBus bus,
            IAppUser appUser,
            IProductQueries productQueries,
            Restaurants.QueryStack.Queries.IRestaurantQueries restaurantQueries)
        {
            _bus = bus;
            _appUser = appUser;
            _productQueries = productQueries;
            _restaurantQueries = restaurantQueries;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromForm] CreateProductCommand command)
        {
            var restaurantId = await _restaurantQueries.GetRestaurantIdByUserIdAsync(_appUser.Id);

            command.SetRestaurantId(restaurantId); 

            return CustomResponse(await _bus.SendAsync(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
            => Ok(await _productQueries.GetProductDetailsByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetProducsAsync()
            => Ok(await _productQueries.GetProductsAsync());
    }
}
