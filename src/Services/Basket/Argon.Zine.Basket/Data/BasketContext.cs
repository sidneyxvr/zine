using Argon.Zine.Basket.Models;
using MongoDB.Driver;

namespace Argon.Zine.Basket.Data;

public class BasketContext
{
    public IMongoCollection<CustomerBasket> CustomerBaskets { get; }

    public BasketContext(IMongoDatabase mongoDatabase)
    {
        CustomerBaskets = mongoDatabase.GetCollection<CustomerBasket>("CustomerBaskets");
    }
}