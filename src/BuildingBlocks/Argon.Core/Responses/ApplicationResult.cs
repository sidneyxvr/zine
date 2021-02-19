using FluentValidation.Results;

namespace Argon.Core.Responses
{
    public class ApplicationResult
    {
        public bool IsValid => ValidationResult?.IsValid ?? false;
        public ValidationResult ValidationResult { get; protected set; }

        public ApplicationResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public static implicit operator ApplicationResult(ValidationResult validationResult)
            => new(validationResult);
    }
}
