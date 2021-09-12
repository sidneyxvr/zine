using Argon.Zine.Identity.Responses;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Argon.Zine.App.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(ValidationResult result)
        {
            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(e => string.IsNullOrWhiteSpace(e.Key) ? 
                        "generic": 
                        char.ToLower(e.Key[0]) + e.Key[1..], e => e.Select(m => m.ErrorMessage).ToArray());
                return BadRequest(new ValidationProblemDetails(errors));
            }

            return Ok();
        }

        protected IActionResult CustomResponse<T>(IdentityResponse<T> response)
        {
            if (!response.ValidationResult.IsValid)
            {
                var errors = response.ValidationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(e => string.IsNullOrWhiteSpace(e.Key) ?
                        "generic" :
                        char.ToLower(e.Key[0]) + e.Key[1..], e => e.Select(m => m.ErrorMessage).ToArray());
                return BadRequest(new ValidationProblemDetails(errors));
            }

            return Ok(response.Result);
        }
    }
}
