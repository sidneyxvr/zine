using Argon.Zine.Basket.Data;
using Argon.Zine.Basket.Models;
using Argon.Zine.Basket.Requests;
using Argon.Zine.Basket.Responses;
using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.Basket.Services;

public class BasketService : IBasketService
{
    private readonly IAppUser _appUser;
    private readonly IBasketDao _basketDao;

    public BasketService(IAppUser appUser, IBasketDao basketDao)
    {
        _appUser = appUser;
        _basketDao = basketDao;
    }

    public async Task<CustomerBasket> AddProductToBasketAsync(ProductToBasketDto product)
    {
        var basket = await _basketDao.GetByCustomerIdAsync(_appUser.Id);

        var basketWasNull = basket is null;

        basket ??= new(product.RestaurantId, 
            product.RestaurantName, product.RestaurantLogoUrl, _appUser.Id);

        basket.AddItem(new(product.Id, product.Name,
            product.Amount, product.Price, product.ImageUrl));

        if (basketWasNull)
        {
            await _basketDao.AddAsync(basket);
        }
        else
        {
            await _basketDao.UpdateAsync(basket);
        }

        return basket;
    }

    public async Task<BasketResponse?> GetBasketAsync(CancellationToken cancellationToken = default)
        => MapToBasketResponse(await _basketDao.GetByCustomerIdAsync(_appUser.Id, cancellationToken));

    public async Task RemoveProductFromBasketAsync(Guid productId)
    {
        var basket = await _basketDao.GetByCustomerIdAsync(_appUser.Id);

        if (basket is null)
        {
            return;
        }

        basket.RemoveItem(productId);

        await _basketDao.UpdateAsync(basket);
    }

    private static BasketResponse? MapToBasketResponse(CustomerBasket? basket)
    {
        if (basket is null)
        {
            return null;
        }
        var products = basket.Products.Select(p
            => new ProductDto(p.Id, p.ProductName, p.Quantity, p.Price, p.ImageUrl));
        
        return new BasketResponse(basket.RestaurantId, basket.RestaurantName,
            basket.RestaurantLogoUrl, basket.Total, products);
    }

    public async Task UpdateBasketItemPriceAsync(Guid basketItemId, decimal price)
        => await _basketDao.UpdateBasketItemPriceAsync(basketItemId, price);
}