using Argon.Basket.Requests;
using Argon.Basket.Responses;
using System;
using System.Threading.Tasks;

namespace Argon.Basket.Services
{
    public interface IBasketService
    {
        Task AddProductToBasket(ProductToBasketDTO product);
        Task RemoveProductFromBasket(Guid productId);
        Task<BasketReponse> GetBasketAsync();
    }
}
