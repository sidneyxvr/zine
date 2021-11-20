using FluentValidation.Results;
using MediatR;

namespace Argon.Zine.Core.Messages;

public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest, ValidationResult>
    where TRequest : Command
{
    protected ValidationResult ValidationResult;

    public RequestHandler()
        => ValidationResult = new();

    public abstract Task<ValidationResult> Handle(TRequest request, CancellationToken cancellationToken);

    public ValidationResult WithError(string propertyName, string errorMessage)
    {
        ValidationResult ??= new ValidationResult();

        ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));

        return ValidationResult;
    }

    public ValidationResult WithError(string errorMessage)
    {
        ValidationResult ??= new ValidationResult();

        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));

        return ValidationResult;
    }
}