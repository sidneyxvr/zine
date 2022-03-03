using Argon.Zine.Commom.Messages;
using FluentValidation;
using MediatR;

namespace Argon.Zine.Commom.Communication;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, AppResult>
    where TRequest : Command
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipeline(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<AppResult> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<AppResult> next)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var validationResult = _validators
            ?.Select(v => v.Validate(request))
            ?.FirstOrDefault();

        return validationResult is { IsValid: false }
            ? AppResult.Failed(validationResult)
            : await next();
    }
}