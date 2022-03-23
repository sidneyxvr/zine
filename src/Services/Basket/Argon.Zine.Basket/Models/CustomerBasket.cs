using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Basket.Models;

public class CustomerBasket : Entity
{
    public Guid CustomerId { get; private set; }
    public Guid RestaurantId { get; private set; }
    public string RestaurantName { get; private set; }
    public string? RestaurantLogoUrl { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public decimal Total => _products.Sum(p => p.Price * p.Quantity);

    private List<BasketItem> _products = new();
    public IReadOnlyCollection<BasketItem> Products
        => _products.AsReadOnly();

#pragma warning disable CS8618 
    private CustomerBasket() { }
#pragma warning restore CS8618 

    public CustomerBasket(Guid restaurantId, string restaurantName,
        string? restaurantLogoUrl, Guid customerId)
    {
        RestaurantId = restaurantId;
        RestaurantName = restaurantName;
        RestaurantLogoUrl = restaurantLogoUrl;
        CustomerId = customerId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(BasketItem item)
    {
        _products ??= new();

        _products.RemoveAll(p => p.Id == item.Id);

        _products.Add(new(item.Id, item.ProductName,
            item.Quantity, item.Price, item.ImageUrl));

        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid productId)
    {
        _products.RemoveAll(p => p.Id == productId);

        UpdatedAt = DateTime.UtcNow;
    }
}