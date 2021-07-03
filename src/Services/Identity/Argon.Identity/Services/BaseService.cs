using FluentValidation.Results;

namespace Argon.Identity.Services
{
    public abstract class BaseService
    {
        protected ValidationResult ValidationResult = new();

        public ValidationResult NotifyError(string error)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));

            return ValidationResult;
        }
    }
}
