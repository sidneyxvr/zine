using FluentValidation.Results;
using MediatR;

namespace Argon.Zine.Commom.Messages;

public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest, AppResult>
    where TRequest : Command
{
    protected ValidationResult ValidationResult;

    public RequestHandler()
        => ValidationResult = new();

    public abstract Task<AppResult> Handle(TRequest request, CancellationToken cancellationToken);


    public AppResult WithError(string propertyName, string errorMessage)
    {
        ValidationResult ??= new ValidationResult();

        ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));

        return AppResult.Failed(ValidationResult);
    }

    public AppResult WithError(string errorMessage)
    {
        ValidationResult ??= new ValidationResult();

        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));

        return AppResult.Failed(ValidationResult);
    }
    
    public AppResult Success(object result)
        => AppResult.Success(result);
}