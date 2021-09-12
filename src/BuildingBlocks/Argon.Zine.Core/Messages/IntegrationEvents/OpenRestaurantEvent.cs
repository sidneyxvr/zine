using System;

namespace Argon.Zine.Core.Messages.IntegrationEvents
{
    public record OpenRestaurantEvent : Event
    {
        public OpenRestaurantEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
