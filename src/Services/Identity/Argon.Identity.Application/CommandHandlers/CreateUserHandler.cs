using Argon.Core.Communication;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Identity.Application.Commands;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Identity.Application.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ValidationResult>
    {
        private readonly IBus _bus;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public CreateUserHandler(
            IBus bus, 
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _bus = bus;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ValidationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await CreateRolesAsync();

            if (!request.IsValid())
            {
                return request.ValidationResult;
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
                result.Errors.ToList().ForEach(e => request.NotifyError(e.Description));
                return request.ValidationResult;
            }

            var validationResult = await _bus.SendAsync(new CreateCustomerCommand(
                user.Id, request.FirstName, request.Surname, request.Email, 
                request.Phone, request.Cpf, request.BirthDate, request.Gender));

            if (!validationResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return validationResult;
        }

        private async Task CreateRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>("Customer"));
                await _roleManager.CreateAsync(new IdentityRole<Guid>("Supplier"));
            }
        }
    }
}
