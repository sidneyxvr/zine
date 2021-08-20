using Argon.Basket.Requests;
using Argon.Basket.Responses;

namespace Argon.Basket.Services
{
    public interface IBasketService
    {
        Task AddProductToBasket(ProductToBasketDTO product);
        Task RemoveProductFromBasket(Guid productId);
        Task<BasketReponse> GetBasketAsync();
        Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price);
    }
}
