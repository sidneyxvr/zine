using Argon.Core.Communication;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Identity.Application.Models;
using Argon.Identity.Application.Responses;
using Argon.Identity.Application.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Identity.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBus _bus;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public AccountService(IBus bus, UserManager<IdentityUser<Guid>> userManager)
        {
            _bus = bus;
            _userManager = userManager;
        }

        public async Task<IdentityResponse> CreateCustomerUserAsync(CustomerUserRequest request)
        {
            var validationResult = new CustomerUserValidation().Validate(request);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var user = new IdentityUser<Guid>
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.Phone,
                LockoutEnabled = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => validationResult.Errors.Add(new ValidationFailure(string.Empty, e.Description)));
                return validationResult;
            }

            var requestResult = await _bus.SendAsync(new CreateCustomerCommand(
                user.Id, request.FirstName, request.Surname, request.Email,
                request.Phone, request.Cpf, request.BirthDate, request.Gender));

            if (!validationResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return validationResult;
        }
    }
}
