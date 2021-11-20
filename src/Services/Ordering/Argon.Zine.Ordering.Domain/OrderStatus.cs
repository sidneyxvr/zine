using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Ordering.Domain;

public class OrderStatus : Entity
{
    public static OrderStatus Submitted => new(OrderStatusAction.Submitted);
    public static OrderStatus Paid => new(OrderStatusAction.Paid);
    public static OrderStatus OrderAccepted => new(OrderStatusAction.OrderAccepted);
    public static OrderStatus Cancelled => new(OrderStatusAction.Cancelled);


    public DateTime ExecutedAt { get; private set; }
    public OrderStatusAction Action { get; private set; }
    public Guid OrderId { get; private set; }

    private OrderStatus() { }

    private OrderStatus(OrderStatusAction action)
        => (Id, ExecutedAt, Action) = (Guid.NewGuid(), DateTime.UtcNow, action);
}