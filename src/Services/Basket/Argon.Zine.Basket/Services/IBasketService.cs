using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Responses;

namespace Argon.Zine.Basket.Services;

public interface IBasketService
{
    Task AddProductToBasketAsync(ProductToBasketDTO product);
    Task RemoveProductFromBasketAsync(Guid productId);
    Task<BasketReponse?> GetBasketAsync(CancellationToken cancellationToken = default);
    Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price);
}