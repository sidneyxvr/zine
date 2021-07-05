using Argon.Catalog.Application.Commands;
using Argon.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        private readonly IBus _bus;

        public DepartmentsController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateDepartmentCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }
    }
}
