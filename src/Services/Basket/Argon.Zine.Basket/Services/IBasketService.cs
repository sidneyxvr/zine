using Argon.Zine.Basket.Models;
using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Responses;

namespace Argon.Zine.Basket.Services;

public interface IBasketService
{
    Task<CustomerBasket> AddProductToBasketAsync(ProductToBasketDto product);
    Task RemoveProductFromBasketAsync(Guid productId);
    Task<BasketResponse?> GetBasketAsync(CancellationToken cancellationToken = default);
    Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price);
}