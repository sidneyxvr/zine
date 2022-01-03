using FluentValidation.Results;

namespace Argon.Zine.Identity.Responses;

public class IdentityResponse<T>
{
    public T? Result { get; private set; }
    public ValidationResult ValidationResult { get; private set; }

    public static implicit operator IdentityResponse<T>(ValidationResult validationResult)
        => new(validationResult);

    public IdentityResponse(ValidationResult validationResult)
        => ValidationResult = validationResult;

    public IdentityResponse(T result)
    {
        Result = result;
        ValidationResult = new ValidationResult();
    }
}

public class IdentityResponse
{
    public ValidationResult ValidationResult { get; private set; }

    public static implicit operator IdentityResponse(ValidationResult validationResult)
        => new(validationResult);
    public IdentityResponse(ValidationResult validationResult)
         => ValidationResult = validationResult;
}