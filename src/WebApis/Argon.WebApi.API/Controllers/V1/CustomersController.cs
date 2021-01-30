using Argon.Core.Communication;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.WebApi.API.Requests;
using Microsoft.AspNetCore.Mvc;
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
            var command = new CreateAddressCommand(request.Street, request.Number,request.District, 
                request.City, request.State, request.Complement, request.PostalCode, request.Complement, 
                request.Latitude, request.Longitude, request.CustomerId);

            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }
    }
}
