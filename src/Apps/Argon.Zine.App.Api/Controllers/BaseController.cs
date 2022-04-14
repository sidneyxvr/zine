using Argon.Zine.Commom;
using Argon.Zine.Identity.Responses;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Argon.Zine.App.Api.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult CustomResponse(AppResult result)
        => result.ValidationResult.IsValid
            ? Ok(result.Result)
            : BadRequest(GetValidationProblemDetails(result.ValidationResult.Errors));

    protected IActionResult CustomResponse(ValidationResult result)
        => result.IsValid
            ? Ok()
            : BadRequest(GetValidationProblemDetails(result.Errors));

    protected IActionResult CustomResponse<T>(IdentityResult<T> response)
        => response.ValidationResult.IsValid
            ? Ok(response.Result)
            : BadRequest(GetValidationProblemDetails(response.ValidationResult.Errors));

    private ValidationProblemDetails GetValidationProblemDetails(List<ValidationFailure> errors)
    {
        var errorsDictionary = errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(e => string.IsNullOrWhiteSpace(e.Key)
                ? "generic"
                : char.ToLower(e.Key[0]) + e.Key[1..], e => e.Select(m => m.ErrorMessage).ToArray());

        var problemDetails = new ValidationProblemDetails(errorsDictionary);
        problemDetails.Extensions["traceId"] = Activity.Current?.TraceId.ToString() ?? HttpContext.TraceIdentifier;

        return problemDetails;
    }
}