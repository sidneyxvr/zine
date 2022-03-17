namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record ClosedRestaurantEvent(Guid AggregateId) : Event(AggregateId);