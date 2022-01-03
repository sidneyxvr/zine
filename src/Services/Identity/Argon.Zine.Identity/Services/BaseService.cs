using FluentValidation.Results;

namespace Argon.Zine.Identity.Services;

public abstract class BaseService
{
    protected ValidationResult ValidationResult = new();

    public ValidationResult WithError(string error)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));

        return ValidationResult;
    }
}