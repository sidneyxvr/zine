using Argon.Core.Communication;
using Argon.Customers.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/supliers")]
    [ApiController]
    public class SuppliersController : BaseController
    {
        private readonly IBus _bus;

        public SuppliersController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddressAsync(UpdateAddressCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }
    }
}
