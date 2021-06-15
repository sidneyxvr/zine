using Argon.Core.Internationalization;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Core.Messages
{
    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest, ValidationResult>
        where TRequest : Command
    {
        protected ValidationResult ValidationResult;
        protected Localizer Localizer;

        public RequestHandler()
        {
            ValidationResult = new();
            Localizer = Localizer.GetLocalizer();
        }

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
}
