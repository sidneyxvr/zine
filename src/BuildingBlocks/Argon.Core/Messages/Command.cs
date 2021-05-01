using FluentValidation.Results;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Argon.Core.Messages
{
    public class Command : Message, IRequest<ValidationResult>
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public void NotifyError(string error)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
