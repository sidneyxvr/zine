using Argon.Zine.Basket.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Basket.Data
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

        public async Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price)
        {
            var filter = Builders<CustomerBasket>.Filter
                .ElemMatch(c => c.Products,
                    Builders<BasketItem>.Filter.Eq(b => b.Id, basketItemId));

            var update = Builders<CustomerBasket>.Update.Set("Products.$.Price", price);

            await _baskets.UpdateManyAsync(filter, update);
        }
    }
}
