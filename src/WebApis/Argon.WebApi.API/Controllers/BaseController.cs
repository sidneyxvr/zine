using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Argon.WebApi.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(ValidationResult result)
        {
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));
            }

            return Ok(result);
        }
    }
}
