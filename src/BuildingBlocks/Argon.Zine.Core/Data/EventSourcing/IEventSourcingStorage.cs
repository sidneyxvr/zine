using Argon.Zine.Core.Messages;

namespace Argon.Zine.Core.Data.EventSourcing;

public interface IEventSourcingStorage
{
    Task AddAsync<TEvent>(TEvent @event) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> GetEventsByAggregateIdAsync(Guid aggregateId);
}