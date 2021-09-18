using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Responses;
using System;
using System.Threading.Tasks;

namespace Argon.Zine.Basket.Services
{
    public interface IBasketService
    {
        Task AddProductToBasket(ProductToBasketDTO product);
        Task RemoveProductFromBasket(Guid productId);
        Task<BasketReponse> GetBasketAsync();
        Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price);
    }
}
