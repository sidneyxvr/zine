using Argon.Zine.Commom.Messages;
using FluentValidation.Results;

namespace Argon.Zine.Commom.Communication;

public interface IBus
{
    Task<ValidationResult> SendAsync<TRequest>(TRequest request) where TRequest : Command;
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
}