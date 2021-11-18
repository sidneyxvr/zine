using Argon.Zine.Core.Communication;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V1
{
    [Route("api/customers")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomersController : BaseController
    {
        private readonly IBus _bus;
        private readonly ICustomerQueries _customerQueries;

        public CustomersController(IBus bus, ICustomerQueries customerQueries)
        {
            _bus = bus;
            _customerQueries = customerQueries;
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressAsync(UpdateCustomerCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpPost("address")]
        [AllowAnonymous]
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

        [HttpGet("addresses")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressesAsync(CancellationToken cancellationToken)
        {
            var customerId = new Guid("144FDB4E-2436-4407-7B0B-08D8E34E905A");
            var result = await _customerQueries.GetAddressesByCustomerIdAsync(customerId, cancellationToken);

            return Ok(result);
        }

        [HttpGet("address/{addressId:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressAsync(Guid addressId, CancellationToken cancellationToken)
        {
            var customerId = new Guid("144FDB4E-2436-4407-7B0B-08D8E34E905A");
            var result = await _customerQueries.GetAddressAsync(customerId, addressId, cancellationToken);

            return Ok(result);
        }
    }
}
