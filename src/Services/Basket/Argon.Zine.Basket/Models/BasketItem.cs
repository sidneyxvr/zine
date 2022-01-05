using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Basket.Models;

public class BasketItem : Entity
{
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string? ImageUrl { get; private set; }
    public BasketItem(Guid id, string productName,
        int quantity, decimal price, string? imageUrl)
    {
        Id = id;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        ImageUrl = imageUrl;
    }
}