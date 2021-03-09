using Argon.Core.Communication;
using Argon.Customers.Application.Commands.AddressCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers.V1
{
    [Route("api/customers")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomersController : BaseController
    {
        private readonly IBus _bus;

        public CustomersController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("address")]
        public async Task<IActionResult> AddAddressAsync(CreateAddressCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpDelete("address/{addressId:Guid}")]
        public async Task<IActionResult> DeleteAddressAsync(Guid addressId)
        {
            var result = await _bus.SendAsync(new DeleteAddressCommand { AddressId = addressId });

            return CustomResponse(result);
        }

        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddressAsync(UpdateAddressCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpPatch("define-main-address")]
        public async Task<IActionResult> DefineMainAddressAsync(DefineMainAddressCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

    }
}
