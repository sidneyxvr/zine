using Argon.Core.Communication;
using Argon.Core.Internationalization;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Identity.Constants;
using Argon.Identity.Models;
using Argon.Identity.Requests;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Argon.Identity.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IBus _bus;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public AccountService(
            IBus bus,
            IEmailService emailService,
            UserManager<User> userManager)
        {
            _bus = bus;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ValidationResult> CreateSupplierUserAsync(SupplierUserRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                LockoutEnabled = true,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => NotifyError(e.Description));
                return ValidationResult;
            }

            var requestResult = await _bus.SendAsync(FromRequestToCommand(request, user.Id));

            if (!requestResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
                return requestResult;
            }

            await _userManager.AddToRoleAsync(user, RoleContant.Supplier);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailConfirmationAccountAsync(request.Email, token);

            return ValidationResult;
        }

        public async Task<ValidationResult> CreateCustomerUserAsync(CustomerUserRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.Phone,
                LockoutEnabled = true,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => NotifyError(e.Description));
                return ValidationResult;
            }

            var requestResult = await _bus.SendAsync(new CreateCustomerCommand
            {
                UserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Cpf = request.Cpf,
                BirthDate = request.BirthDate,
                Gender = request.Gender
            });

            if (!requestResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
                return requestResult;
            }

            await _userManager.AddToRoleAsync(user, RoleContant.Customer);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailConfirmationAccountAsync(request.Email, token);

            return ValidationResult;
        }

        public async Task<ValidationResult> ConfirmEmailAccountAsync(EmailAccountConfirmationRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return NotifyError(Localizer.GetTranslation("CannotConfirmEmailAccount"));
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            return result.Succeeded ?
                ValidationResult :
                NotifyError(Localizer.GetTranslation("CannotConfirmEmailAccount"));
        }

        public async Task<ValidationResult> ResendConfirmEmailAccountAsync(EmailRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return NotifyError(Localizer.GetTranslation("CannotResendConfirmEmailAccount"));
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailConfirmationAccountAsync(user.Email, token);

            return ValidationResult;
        }

        public async Task<ValidationResult> SendResetPasswordAsync(EmailRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
            {
                return NotifyError(Localizer.GetTranslation("CannotSendResetPassword"));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendEmailResetPasswordAsync(user.Email, token);

            return ValidationResult;
        }

        public async Task<ValidationResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
            {
                return NotifyError(Localizer.GetTranslation("CannotResetPassword"));
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return result.Succeeded ?
                ValidationResult :
                NotifyError(Localizer.GetTranslation("CannotResetPassword"));
        }

        private static CreateSupplierCommand FromRequestToCommand(SupplierUserRequest request, Guid userId)
        {
            return new CreateSupplierCommand
            {
                UserId = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                City = request.City,
                Complement = request.Complement,
                CorparateName = request.CorparateName,
                CpfCnpj = request.CpfCnpj,
                District = request.District,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Number = request.Number,
                PostalCode = request.PostalCode,
                State = request.State,
                Street = request.Street,
                TradeName = request.TradeName,
            };
        }
    }
}
