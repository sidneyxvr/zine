using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace Argon.Zine.Commom.Messages;

public record Command : Message, IRequest<ValidationResult>
{
    [JsonIgnore]
    public DateTime Timestamp { get; private set; }

    protected Command()
        => Timestamp = DateTime.UtcNow;
}