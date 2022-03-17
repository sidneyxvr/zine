namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record ChangedProductPriceEvent(Guid AggregateId, decimal Price) : Event(AggregateId);