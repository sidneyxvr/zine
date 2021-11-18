using Argon.Zine.Basket.Models;

namespace Argon.Zine.Basket.Data
{
    public interface IBasketDAO
    {
        Task AddAsync(CustomerBasket basket);
        Task UpdateAsync(CustomerBasket basket);
        Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price);
        Task<CustomerBasket?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
