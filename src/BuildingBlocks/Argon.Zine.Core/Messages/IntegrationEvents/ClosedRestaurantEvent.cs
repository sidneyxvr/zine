namespace Argon.Zine.Core.Messages.IntegrationEvents;

public record ClosedRestaurantEvent : Event
{
    public ClosedRestaurantEvent(Guid aggregateId)
        => AggregateId = aggregateId;
}