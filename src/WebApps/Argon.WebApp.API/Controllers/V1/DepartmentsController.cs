using Argon.Catalog.Application.Commands;
using Argon.Catalog.QueryStack.Queries;
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
        private readonly IDepartmentQuery _departmentQuery;

        public DepartmentsController(
            IBus bus,
            IDepartmentQuery departmentQuery)
        {
            _bus = bus;
            _departmentQuery = departmentQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateDepartmentCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _departmentQuery.GetAllAsync());
        }
    }
}
