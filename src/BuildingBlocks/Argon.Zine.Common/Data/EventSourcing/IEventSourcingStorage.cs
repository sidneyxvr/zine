using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Commom.Data.EventSourcing;

public interface IEventSourcingStorage
{
    Task AddAsync<TEvent>(TEvent @event) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> GetEventsByAggregateIdAsync(Guid aggregateId);
}