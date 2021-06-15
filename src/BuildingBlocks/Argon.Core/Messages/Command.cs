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

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
