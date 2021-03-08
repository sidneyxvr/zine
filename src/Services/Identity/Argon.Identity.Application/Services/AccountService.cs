using Argon.Core.Communication;
using Argon.Core.Internationalization;
using Argon.Core.Messages.IntegrationCommands;
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
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public AccountService(
            IBus bus, 
            IEmailService emailService, 
            UserManager<IdentityUser<Guid>> userManager)
        {
            _bus = bus;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ValidationResult> CreateCustomerUserAsync(CustomerUserRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
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
                result.Errors.ToList().ForEach(e => NotifyError(e.Description));
                return ValidationResult;
            }

            var requestResult = await _bus.SendAsync(new CreateCustomerCommand(
                user.Id, request.FirstName, request.Surname, request.Email, 
                request.Phone, request.Cpf, request.BirthDate, request.Gender));

            if (!requestResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
                return requestResult;
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailConfirmationAccountAsync(request.Email, token);

            return ValidationResult;
        }

        public async Task<ValidationResult> ConfirmAccountEmailAsync(AccountEmailConfirmationRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                return NotifyError(Localizer.GetTranslation("CannotConfirmEmailAccount"));
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            return result.Succeeded ? 
                ValidationResult : 
                NotifyError(Localizer.GetTranslation("CannotConfirmEmailAccount"));
        }

        public async Task<ValidationResult> ResendConfirmAccountEmailAsync(EmailRequest request)
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

        public async Task<ValidationResult> UpdateTwoFactorAuthenticationAsync(UpdateTwoFactorAuthenticationRequest request)
        {
            if (IsInvalid(request))
            {
                return ValidationResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
            {
                return NotifyError(request.EnableTwoFactorAuthentication ?
                    Localizer.GetTranslation("CannotEnableTwoFactorAuthentication") :
                    Localizer.GetTranslation("CannotDisableTwoFactorAuthentication"));
            }

            var result = await _userManager.SetTwoFactorEnabledAsync(user, request.EnableTwoFactorAuthentication);

            return result.Succeeded ? 
                ValidationResult : 
                NotifyError(request.EnableTwoFactorAuthentication ?
                    Localizer.GetTranslation("CannotEnableTwoFactorAuthentication") :
                    Localizer.GetTranslation("CannotDisableTwoFactorAuthentication"));
        }
    }
}
