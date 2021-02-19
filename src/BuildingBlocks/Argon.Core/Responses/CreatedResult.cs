using FluentValidation.Results;

namespace Argon.Core.Responses
{
    public class CreatedResult<T> : ApplicationResult
    {
        public T Id { get; private set; }

        public CreatedResult(T id, ValidationResult validationResult)
            : base(validationResult)
        {
            Id = id;
        }

        public CreatedResult(ValidationResult validationResult)
            : base(validationResult)
        {
        }
    }
}
