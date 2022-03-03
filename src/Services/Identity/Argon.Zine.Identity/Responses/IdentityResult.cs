using FluentValidation.Results;

namespace Argon.Zine.Identity.Responses;

public class IdentityResult<T>
{
    public T? Result { get; private set; }
    public ValidationResult ValidationResult { get; private set; }

    public static implicit operator IdentityResult<T>(ValidationResult validationResult)
        => new(validationResult);

    public IdentityResult(ValidationResult validationResult)
        => ValidationResult = validationResult;

    public IdentityResult(T result)
    {
        Result = result;
        ValidationResult = new ValidationResult();
    }
}

public class IdentityResult
{
    public ValidationResult ValidationResult { get; private set; }

    public static implicit operator IdentityResult(ValidationResult validationResult)
        => new(validationResult);
    public IdentityResult(ValidationResult validationResult)
         => ValidationResult = validationResult;
}