using Argon.Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Argon.Core.Communication
{
    public interface IBus
    {
        Task<ValidationResult> SendAsync<TRequest>(TRequest request) where TRequest : Command;
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}
