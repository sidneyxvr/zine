using Argon.Zine.Commom.Data.EventSourcing;
using Argon.Zine.Commom.Messages;
using FluentValidation.Results;
using MediatR;

namespace Argon.Zine.Commom.Communication;

public class InMemoryBus : IBus
{
    private readonly IMediator _mediator;
    private readonly IEventSourcingStorage _eventSourcingStorage;

    public InMemoryBus(
        IMediator mediator,
        IEventSourcingStorage eventSourcingStorage)
    {
        _mediator = mediator;
        _eventSourcingStorage = eventSourcingStorage;
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
    {
        await _mediator.Publish(@event);

        await _eventSourcingStorage.AddAsync(@event);
    }

    public async Task<ValidationResult> SendAsync<TRequest>(TRequest request) where TRequest : Command
        => await _mediator.Send(request);
}