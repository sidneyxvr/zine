using Argon.Core.Messages;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Core.Communication
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, ValidationResult> 
        where TRequest : Command 
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationPipeline(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<ValidationResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ValidationResult> next)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            return await next();
        }
    }
}
