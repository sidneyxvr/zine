namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record OpenRestaurantEvent(Guid AggregateId) : Event(AggregateId);