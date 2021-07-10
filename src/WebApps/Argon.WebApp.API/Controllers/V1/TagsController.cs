using Argon.Catalog.Application.Commands;
using Argon.Catalog.Application.Queries;
using Argon.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : BaseController
    {
        private readonly IBus _bus;
        private readonly ITagQueries _tagQuery;

        public TagsController(
            IBus bus, 
            ITagQueries tagQuery)
        {
            _bus = bus;
            _tagQuery = tagQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTagCommand command)
        {
            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _tagQuery.GetAllAsync());
        }
    }
}
