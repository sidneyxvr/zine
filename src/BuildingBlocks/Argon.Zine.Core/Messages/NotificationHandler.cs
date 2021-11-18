using MediatR;

namespace Argon.Zine.Core.Messages;

public abstract class NotificationHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : Event
{
    public abstract Task Handle(TNotification notification, CancellationToken cancellationToken);
}