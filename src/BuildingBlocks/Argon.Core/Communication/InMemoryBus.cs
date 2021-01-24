using Argon.Core.Messages;
using Argon.Core.Messages.Events;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;

namespace Argon.Core.Communication
{
    public class InMemoryBus : IBus
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendAsync<TRequest>(TRequest request) where TRequest : Command
        {
            return await _mediator.Send(request);
        }
    }
}
