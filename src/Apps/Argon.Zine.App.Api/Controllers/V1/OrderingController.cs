using Argon.Zine.Basket.Services;
using Argon.Zine.Commom.Communication;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Customers.Application.Queries;
using Argon.Zine.Ordering.Application.Commands;
using Argon.Zine.Ordering.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Argon.Zine.App.Api.Controllers.V1;

[Route("api/ordering")]
[ApiController]
public class OrderingController : BaseController
{
    private readonly IBus _bus;
    private readonly IAppUser _appUser;
    private readonly IBasketService _basketService;
    private readonly ICustomerQueries _customerQueries;

    public OrderingController(
        IBus bus,
        IAppUser appUser,
        IBasketService basketService,
        ICustomerQueries customerQueries)
    {
        _bus = bus;
        _appUser = appUser;
        _customerQueries = customerQueries;
        _basketService = basketService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitOrderAsync(SubmitOrderRequest request)
    {
        var address = (await _customerQueries.GetAddressAsync(_appUser.Id, request.AddressId))!;
        var addressDto = new AddressDto(address.Street, address.Number, address.Country, address.City, 
            address.State, address.Country, address.PostalCode, address.Complement );
        
        var basket = (await _basketService.GetBasketAsync())!;

        var orderItems = basket.Products.Select(p
            => new OrderItemDto(p.Id, p.Name, p.ImageUrl, p.Price, p.Amount));

        var commad = new SubmitOrderCommand(_appUser.Id, request.PaymentMethodId, 
            basket.RestaurantId, addressDto, orderItems);

        var result = await _bus.SendAsync(commad);

        return CustomResponse(result);
    }
}