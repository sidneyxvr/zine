using Argon.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argon.Core.Data.EventSourcing
{
    public interface IEventSourcingStorage
    {
        Task AddAsync<TEvent>(TEvent @event) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEventsByAggregateIdAsync(Guid aggregateId);
    }
}
