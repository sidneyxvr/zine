using Argon.Zine.Basket.Data;
using Argon.Zine.Basket.Models;
using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Responses;
using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Basket.Services;

public class BasketService : IBasketService
{
    private readonly IAppUser _appUser;
    private readonly IBasketDAO _Basket;

    public BasketService(IAppUser appUser, IBasketDAO Basket)
    {
        _appUser = appUser;
        _Basket = Basket;
    }

    public async Task AddProductToBasketAsync(ProductToBasketDTO product)
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

    public async Task<BasketReponse?> GetBasketAsync(CancellationToken cancellationToken = default)
        => MapToBasketReponse(await _Basket.GetByCustomerIdAsync(_appUser.Id, cancellationToken));

    public async Task RemoveProductFromBasketAsync(Guid productId)
    {
        var basket = await _Basket.GetByCustomerIdAsync(_appUser.Id);

        if (basket is null)
        {
            return;
        }

        basket.RemoveItem(productId);

        await _Basket.UpdateAsync(basket);
    }

    private static BasketReponse? MapToBasketReponse(CustomerBasket? basket)
        => basket is not null
        ? new()
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
                Amount = p.Quantity,
                ImageUrl = p.ImageUrl!,
            })
        }
       : null;

    public async Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price)
        => await _Basket.UpdateBasketItemPriceAsync(basketItemId, price);
}