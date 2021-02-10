using Argon.Core.Communication;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.WebApi.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.WebApi.API.Controllers.V1
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : BaseController
    {
        private readonly IBus _bus;

        public CustomersController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("address")]
        public async Task<IActionResult> AddAddressAsync(AddressRequest request)
        {
            var customerId = Guid.NewGuid();

            var command = new CreateAddressCommand(customerId, request.Street, request.Number,
                request.District, request.City, request.State, request.Complement, request.PostalCode, 
                request.Complement, request.Latitude, request.Longitude);

            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpDelete("address/{addressId:Guid}")]
        public async Task<IActionResult> DeleteAddressAsync(Guid addressId)
        {
            var customerId = Guid.NewGuid();
            
            var result = await _bus.SendAsync(new DeleteAddressCommand(customerId, addressId));

            return CustomResponse(result);
        }

        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddressAsync(AddressRequest request)
        {
            var customerId = Guid.NewGuid();

            var command = new UpdateAddressCommand(customerId, request.Id, request.Street, request.Number, 
                request.District, request.City, request.State, request.Complement, request.PostalCode, 
                request.Complement, request.Latitude, request.Longitude);

            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpPatch("define-main-address")]
        public async Task<IActionResult> DefineMainAddressAsync(AddressRequest request)
        {
            var customerId = Guid.NewGuid();
            var command = new DefineMainAddressCommand(customerId, request.Id);

            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

    }
}
