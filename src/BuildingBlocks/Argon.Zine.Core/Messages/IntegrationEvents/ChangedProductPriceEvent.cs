namespace Argon.Zine.Commom.Messages.IntegrationEvents;

public record ChangedProductPriceEvent : Event
{
    public decimal Price { get; private set; }

    public ChangedProductPriceEvent(Guid aggregateId, decimal price)
        => (AggregateId, Price) = (aggregateId, price);
}