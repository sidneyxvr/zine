using Argon.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Argon.Basket.Models
{
    public class Basket : Entity
    {
        public Guid RestaurantId { get; private set; }
        public string RestaurantName { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private List<Product> _products = new();
        public IReadOnlyCollection<Product> Products
            => _products.AsReadOnly();

        public Basket(Guid restaurantId, string restaurantName, Guid customerId)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddProduct(Product product)
        {
            _products ??= new();

            _products.RemoveAll(p => p.Id == product.Id);

            _products.Add(new(product.Id, product.Name, 
                product.Amount, product.Price, product.ImageUrl));
        }

        public void RemoveProduct(Guid productId)
            => _products.RemoveAll(p => p.Id == productId);
    }
}