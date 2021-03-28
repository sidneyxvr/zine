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
    }
}
