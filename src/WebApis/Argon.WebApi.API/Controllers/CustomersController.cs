using Argon.Core.Communication;
using Argon.Customers.Application.Commands.CustomerCommands;
using Argon.WebApi.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IBus _bus;

        public CustomersController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CustomerRequest request)
        {
            var result = await _bus.SendAsync(new CreateCustomerCommand(
                Guid.NewGuid(), request.FullName, request.Email, request.Phone, request.Cpf, request.BirthDate, request.Gender));

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
