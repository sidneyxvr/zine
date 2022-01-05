namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record ClosedRestaurantEvent : Event
{
    public ClosedRestaurantEvent(Guid aggregateId)
        => AggregateId = aggregateId;
}