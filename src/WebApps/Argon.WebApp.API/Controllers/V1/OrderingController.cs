﻿using Argon.Basket.Services;
using Argon.Core.Communication;
using Argon.Core.DomainObjects;
using Argon.Customers.Application.Queries;
using Argon.Ordering.Application.Commands;
using Argon.Ordering.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
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
            var address = (await _customerQueries.GetAddressByCustomerIdAsync(_appUser.Id, request.AddressId))!;
            var basket = (await _basketService.GetBasketAsync())!;

            var commad = new SubmitOrderCommand
            {
                City = address.City,
                Complement = address.Complement,
                Country = address.Country,
                District = address.District,
                Number = address.Number,
                State = address.State,
                Street = address.Street,
                PostalCode = address.PostalCode,
                CustomerId = _appUser.Id,
                RestaurantId = basket.RestaurantId,
                PaymentMethodId = request.PaymentMethodId,
                OrderItems = basket.Products
                    .Select(p => new OrderItemDTO 
                    { 
                        ProductId = p.Id,
                        ProductImageUrl = p.ImageUrl,
                        ProductName = p.Name,
                        UnitPrice = p.Price,
                        Units = p.Amount
                    })
            };

            var result = await _bus.SendAsync(commad);

            return CustomResponse(result);
        }
    }
}