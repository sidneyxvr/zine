using Argon.Zine.Catalog.QueryStack.Models;
using Argon.Zine.Catalog.QueryStack.Services;
using Argon.Zine.Catalog.Shared.Response;
using Argon.Zine.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Infra.Data.Queries.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<CatalogDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _products = database.GetCollection<Product>("Products");
        }

        public async Task AddAsync(Product product)
            => await _products.InsertOneAsync(product);

        public async Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
            => await _products.Find(Builders<Product>.Filter.Eq(p => p.Id, id))
                .Project(p => new ProductBasketResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    RestaurantId = p.Restaurant.Id,
                    RestaurantName = p.Restaurant.Name,
                    RestaurantLogoUrl = p.Restaurant.LogoUrl
                })
                .FirstOrDefaultAsync();

        public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
            => await _products.Find(Builders<Product>.Filter.Eq(p => p.Id, id))
                .Project(p => new ProductDetailsResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .FirstOrDefaultAsync();

        public async Task<PagedList<ProductDetailsResponse>> GetProductsAsync()
        {
            var list = await _products.Find(Builders<Product>.Filter.Empty)
            .Project(p => new ProductDetailsResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToListAsync();

            var count = await _products.CountDocumentsAsync(Builders<Product>.Filter.Empty);

            return new(list, count);
        }
    }
}