using Argon.Core.Internationalization;
using Argon.Identity.Requests;
using FluentValidation.Results;

namespace Argon.Identity.Services
{
    public abstract class BaseService
    {
        protected Localizer Localizer;
        protected ValidationResult ValidationResult; 

        protected BaseService()
        {
            Localizer = Localizer.GetLocalizer();
        }

        protected bool IsValid(BaseRequest request)
        {
            ValidationResult = request.Validate();
            return ValidationResult.IsValid;
        }

        protected bool IsInvalid(BaseRequest request)
        {
            ValidationResult = request.Validate();
            return !ValidationResult.IsValid;
        }

        public ValidationResult NotifyError(string error)
        {
            ValidationResult ??= new ValidationResult();

            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));

            return ValidationResult;
        }

        public ValidationResult NotifyError(string propertyName, string error)
        {
            ValidationResult ??= new ValidationResult();

            ValidationResult.Errors.Add(new ValidationFailure(propertyName, error));

            return ValidationResult;
        }

        public ValidationResult NotifyError(ValidationResult validationResult)
        {
            ValidationResult ??= new ValidationResult();

            foreach(var failure in validationResult.Errors)
            {
                ValidationResult.Errors.Add(failure);
            }

            return ValidationResult;
        }
    }
}
