using Argon.Core.Communication;
using Argon.Core.DomainObjects;
using Argon.Customers.Application.Queries;
using Argon.Ordering.Application.Commands;
using Argon.Ordering.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argon.WebApp.API.Controllers.V1
{
    [Route("api/buyers")]
    [ApiController]
    public class BuyersController : BaseController
    {
        private readonly IBus _bus;
        private readonly IAppUser _appUser;
        private readonly ICustomerQueries _customerQueries;

        public BuyersController(
            IBus bus, 
            IAppUser appUser, 
            ICustomerQueries customerQueries)
        {
            _bus = bus;
            _appUser = appUser;
            _customerQueries = customerQueries;
        }

        [HttpPost("payment-method")]
        public async Task<IActionResult> AddPaymentMethodAsync(AddPaymentMethodRequest request)
        {
            var customer = (await _customerQueries.GetCustomerNameByIdAsync(_appUser.Id))!;

            var command = new AddPaymentMethodCommand
            {
                CustomerId = customer.Id,
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                Alias = request.Alias,
                CardHolderName = request.CardHolderName,
                CardNamber = request.CardNamber,
                Expiration = request.Expiration,
            };

            var result = await _bus.SendAsync(command);

            return CustomResponse(result);
        }
    }
}
