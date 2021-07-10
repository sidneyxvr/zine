using Argon.Core.Communication;
using Argon.Customers.Application.Commands;
using Argon.Customers.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/customers")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomersController : BaseController
    {
        private readonly IBus _bus;
        private readonly ICustomerQueries _customerQuery;

        public CustomersController(IBus bus, ICustomerQueries customerQuery)
        {
            _bus = bus;
            _customerQuery = customerQuery;
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
        public async Task<IActionResult> GetAddressesAsync()
        {
            var result = await _customerQuery.GetAddressesByCustomerId(new Guid("144FDB4E-2436-4407-7B0B-08D8E34E905A"));

            return Ok(result);
        }

        [HttpGet("address/{addressId:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressAsync(Guid addressId)
        {
            var customerId = new Guid("144FDB4E-2436-4407-7B0B-08D8E34E905A");
            var result = await _customerQuery.GetAddressByCustomerId(customerId, addressId);

            return Ok(result);
        }
    }
}
