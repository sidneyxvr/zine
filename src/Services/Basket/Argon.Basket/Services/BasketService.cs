using Argon.Basket.Data;
using Argon.Basket.Models;
using Argon.Basket.Requests;
using Argon.Basket.Responses;
using Argon.Core.DomainObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAppUser _appUser;
        private readonly IBasketDAO _Basket;

        public BasketService(IAppUser appUser, IBasketDAO Basket)
        {
            _appUser = appUser;
            _Basket = Basket;
        }

        public async Task AddProductToBasket(ProductToBasketDTO product)
        {
            var basket = await _Basket.GetByCustomerIdAsync(_appUser.Id);

            var basketWasNull = basket is null;

            basket ??=
                new CustomerBasket(product.RestaurantId, product.RestaurantName, 
                    product.RestaurantLogoUrl, _appUser.Id);

            basket.AddItem(new(product.Id, product.Name, 
                product.Amount, product.Price, product.ImageUrl));

            if (basketWasNull)
            {
                await _Basket.AddAsync(basket);
            }
            else
            {
               await _Basket.UpdateAsync(basket);
            }
        }

        public async Task<BasketReponse> GetBasketAsync()
            => MapToBasketReponse(await _Basket.GetByCustomerIdAsync(_appUser.Id));

        public async Task RemoveProductFromBasket(Guid productId)
        {
            var basket = await _Basket.GetByCustomerIdAsync(_appUser.Id);

            if(basket is null)
            {
                return;
            }

            basket.RemoveItem(productId);

            await _Basket.UpdateAsync(basket);
        }

        private static BasketReponse MapToBasketReponse(CustomerBasket basket)
            => new()
            {
                RestaurantId = basket.RestaurantId,
                RestaurantName = basket.RestaurantName,
                RestaurantLogoUrl = basket.RestaurantLogoUrl,
                Total = basket.Total,
                Products = basket.Products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.ProductName,
                    Price = p.Price,
                    Amount= p.Quantity,
                    ImageUrl= p.ImageUrl,   
                })
            };
    }
}
