using Argon.Zine.Basket.Models;
using MongoDB.Driver;

namespace Argon.Zine.Basket.Data;

public class BasketDao : IBasketDao
{
    private readonly BasketContext _context;

    public BasketDao(BasketContext context)
        => _context = context;

    public async Task AddAsync(CustomerBasket basket)
        => await _context.CustomerBaskets.InsertOneAsync(basket);

    public async Task<CustomerBasket?> GetByCustomerIdAsync(
        Guid customerId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<CustomerBasket>.Filter.Eq(b => b.CustomerId, customerId);
        return await _context.CustomerBaskets.Find(filter).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(CustomerBasket basket)
        => await _context.CustomerBaskets.ReplaceOneAsync(b => b.Id == basket.Id, basket);

    public async Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price)
    {
        var filter = Builders<CustomerBasket>.Filter
            .ElemMatch(c => c.Products,
                Builders<BasketItem>.Filter.Eq(b => b.Id, basketItemId));

        var update = Builders<CustomerBasket>.Update.Set("Products.$.Price", price);

        await _context.CustomerBaskets.UpdateManyAsync(filter, update);
    }
}