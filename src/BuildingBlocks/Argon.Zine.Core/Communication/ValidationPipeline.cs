using Argon.Zine.Core.Messages;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Core.Communication
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, ValidationResult> 
        where TRequest : Command 
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<ValidationResult> Handle(
            TRequest request,
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<ValidationResult> next)
        {
            var validationResult = _validators
                ?.Select(v => v.Validate(request))
                ?.FirstOrDefault();

            if (validationResult?.IsValid == false)
            {
                return validationResult;
            }

            return await next();
        }
    }
}
