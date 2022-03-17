using MediatR;

namespace Argon.Zine.Commom.Messages;

public abstract record Event(Guid AggregateId) : Message, INotification
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}