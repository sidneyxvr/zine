namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record OpenRestaurantEvent : Event
{
    public OpenRestaurantEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}