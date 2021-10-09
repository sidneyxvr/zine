using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Core.Communication;
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
        private readonly IProductQueries _productQueries;

        public ProductsController(
            IBus bus,
            IProductQueries productQueries)
        {
            _bus = bus;
            _productQueries = productQueries;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromForm] CreateProductCommand command)
            => CustomResponse(await _bus.SendAsync(command));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
            => Ok(await _productQueries.GetProductDetailsByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetProducsAsync()
            => Ok(await _productQueries.GetProductsAsync());
    }
}
