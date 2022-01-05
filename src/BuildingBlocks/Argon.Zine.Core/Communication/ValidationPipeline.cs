using Argon.Zine.Commom.Messages;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Argon.Zine.Commom.Communication;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, ValidationResult>
    where TRequest : Command
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipeline(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<ValidationResult> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<ValidationResult> next)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var validationResult = _validators
            ?.Select(v => v.Validate(request))
            ?.FirstOrDefault();

        return validationResult is null or { IsValid: true } 
            ? await next()
            : validationResult;
    }
}