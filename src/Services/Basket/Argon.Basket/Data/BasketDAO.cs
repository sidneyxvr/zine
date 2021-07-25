using Argon.Basket.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Argon.Basket.Data
{
    public class BasketDAO : IBasketDAO
    {
        private readonly IMongoCollection<CustomerBasket> _baskets;

        public BasketDAO(IOptions<BasketDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _baskets = database.GetCollection<CustomerBasket>("Baskets");
        }

        public async Task AddAsync(CustomerBasket basket)
            => await _baskets.InsertOneAsync(basket);

        public async Task<CustomerBasket> GetByCustomerIdAsync(Guid customerId)
        {
            var filter = Builders<CustomerBasket>.Filter.Eq(b => b.CustomerId, customerId);
            return await _baskets.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(CustomerBasket basket)
            => await _baskets.ReplaceOneAsync(b => b.Id == basket.Id, basket);
    }
}
