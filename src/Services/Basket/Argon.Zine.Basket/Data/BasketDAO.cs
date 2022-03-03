using Argon.Zine.Basket.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        => await _context.CustomerBaskets.AsQueryable()
            .SingleOrDefaultAsync(b => b.CustomerId == customerId, cancellationToken);

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