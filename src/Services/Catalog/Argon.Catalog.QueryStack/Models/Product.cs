using System;

namespace Argon.Catalog.QueryStack.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string? ImageUrl { get; private set; }

        public RestaurantProduct Restaurant { get; set; }

        public Product(Guid id, string name, decimal price, 
            string? imageUrl, RestaurantProduct restaurant)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Restaurant = restaurant;
        }
    }

    public class RestaurantProduct
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? LogoUrl { get; private set; }

        public RestaurantProduct(Guid id, string name, string? logoUrl)
        {
            Id = id;
            Name = name;
            LogoUrl = logoUrl;
        }
    }
}
