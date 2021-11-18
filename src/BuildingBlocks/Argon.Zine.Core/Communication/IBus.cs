using Argon.Zine.Core.Messages;
using FluentValidation.Results;

namespace Argon.Zine.Core.Communication;

public interface IBus
{
    Task<ValidationResult> SendAsync<TRequest>(TRequest request) where TRequest : Command;
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
}