using System;

namespace Argon.Core.Messages.IntegrationEvents
{
    public record OpenRestaurantEvent : Event
    {
        public OpenRestaurantEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
