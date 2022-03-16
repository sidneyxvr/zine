using Argon.Zine.Commom.DomainObjects;
using FluentValidation.Results;

namespace Argon.Zine.Commom;

public class AppResult
{
    public object? Result { get; }
    public ValidationResult ValidationResult { get; }

    private AppResult(object? result, ValidationResult validationResult)
    {
        Result = result;
        ValidationResult = validationResult;
    }

    public static AppResult Success(object? result = null)
        => new(result, new ValidationResult());

    public static AppResult Failed(ValidationResult validationResult)
        => new(null, validationResult);

    public static implicit operator AppResult(Entity? result)
        => new(result, new ValidationResult());
}