﻿using Argon.Catalog.Application.Commands;
using Argon.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly IBus _bus;

        public CategoriesController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }
    }
}
