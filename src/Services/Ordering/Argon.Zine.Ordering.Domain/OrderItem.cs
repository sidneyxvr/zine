using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Ordering.Domain;

public class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductImageUrl { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Units { get; private set; }
    public Guid OrderId { get; private set; }

#pragma warning disable CS8618
    private OrderItem() { }
#pragma warning restore CS8618

    public OrderItem(Guid productId, string productName,
        string productImageUrl, decimal unitPrice, int units)
    {
        ProductId = productId;
        ProductName = productName;
        ProductImageUrl = productImageUrl;
        UnitPrice = unitPrice;
        Units = units;
    }
}