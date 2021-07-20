using Argon.Catalog.QueryStack.Models;
using Argon.Catalog.QueryStack.Services;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Argon.Catalog.Infra.Data.Queries.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMongoCollection<Restaurant> _restaurants;

        public RestaurantService(CatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _restaurants = database.GetCollection<Restaurant>(Pluralizer.Pluralize(nameof(Restaurant)));
        }

        public async Task AddAsync(Restaurant restaurant)
            => await _restaurants.InsertOneAsync(restaurant);

        public async Task<Restaurant?> GetByIdAsync(Guid id)
            => await _restaurants.Find(Builders<Restaurant>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();

        public async Task UpdateAsync(Restaurant restaurant)
            => await _restaurants.ReplaceOneAsync(r => r.Id == restaurant.Id, restaurant);
    }
}
