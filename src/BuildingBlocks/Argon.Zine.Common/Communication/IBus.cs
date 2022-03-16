using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Commom.Communication;

public interface IBus
{
    Task<AppResult> SendAsync<TRequest>(TRequest request) where TRequest : Command;
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
}