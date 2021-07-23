using Argon.Core.DomainObjects;
using System;

namespace Argon.Basket.Models
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public decimal Price { get; private set; }
        public string? ImageUrl { get; private set; }
        public Product(Guid id, string name, 
            int amount, decimal price, string? imageUrl)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Price = price;
            ImageUrl = imageUrl;
        }
    }
}
