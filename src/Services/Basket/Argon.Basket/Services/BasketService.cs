using Argon.Basket.Data;
using Argon.Basket.Requests;
using Argon.Core.DomainObjects;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Argon.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAppUser _appUser;
        private readonly IMongoCollection<Models.Basket> _baskets;

        public BasketService(
            IAppUser appUser, 
            IOptions<BasketDatabaseSettings> settings)
        {
            _appUser = appUser;

            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _baskets = database.GetCollection<Models.Basket>("Baskets");
        }

        public async Task<ValidationResult> AddProductToBasket(ProductToBasketDTO product)
        {
            var filter = Builders<Models.Basket>.Filter.Eq(b => b.CustomerId, _appUser.Id);
            var basket = await _baskets.Find(filter).SingleOrDefaultAsync();

            var basketWasNull = basket is null;
            
            basket ??= 
                new Models.Basket(product.RestaurantId, product.RestaurantName, _appUser.Id);

            basket.AddProduct(new(product.Id, product.Name, 
                product.Amount, product.Price, product.ImageUrl));

            if (basketWasNull)
            {
                await _baskets.InsertOneAsync(basket);
            }
            else
            {
               await _baskets.ReplaceOneAsync(b => b.Id == basket.Id, basket);
            }

            return new();
        }
    }
}
