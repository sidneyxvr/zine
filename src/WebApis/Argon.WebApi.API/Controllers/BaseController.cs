using Argon.Identity.Application.Responses;
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
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(e => char.ToLower(e.Key[0]) + e.Key[1..], e => e.Select(m => m.ErrorMessage).ToArray());
                return BadRequest(new ValidationProblemDetails(errors));
            }

            return Ok();
        }

        protected IActionResult CustomResponse(IdentityResponse result)
        {
            if (!result.ValidationResult.IsValid)
            {
                var errors = result.ValidationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(e => char.ToLower(e.Key[0]) + e.Key[1..], e => e.Select(m => m.ErrorMessage).ToArray());
                return BadRequest(new ValidationProblemDetails(errors));
            }

            return Ok();
        }
    }
}
