using Argon.Zine.Commom.Communication;
using Argon.Zine.Commom.Messages.IntegrationCommands;
using Argon.Zine.Identity.Constants;
using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Requests;
using Argon.Zine.Identity.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Identity.Services;

public class AccountService : BaseService, IAccountService
{
    private readonly IBus _bus;
    private readonly IStringLocalizer _localizer;
    private readonly IEmailService _emailService;
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizerFactory _localizerFactory;

    public AccountService(
        IBus bus,
        IEmailService emailService,
        UserManager<User> userManager,
        IStringLocalizerFactory localizerFactory)
    {
        _bus = bus;
        _userManager = userManager;
        _emailService = emailService;
        _localizerFactory = localizerFactory;
        _localizer = localizerFactory.Create(typeof(AccountService));
    }

    public async Task<ValidationResult> CreateRestaurantUserAsync(RestaurantUserRequest request)
    {
        var localizer = _localizerFactory.Create(typeof(RestaurantUserValidator));
        var validationResult = new RestaurantUserValidator(localizer).Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            LockoutEnabled = true,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(e => WithError(e.Description));
            return ValidationResult;
        }

        var requestResult = await _bus.SendAsync(FromRequestToCommand(request, user.Id));

        if (!requestResult.ValidationResult.IsValid)
        {
            await _userManager.DeleteAsync(user);
            return requestResult.ValidationResult;
        }

        await _userManager.AddToRoleAsync(user, RoleContant.Restaurant);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _emailService.SendEmailConfirmationAccountAsync(request.Email, token);

        return ValidationResult;
    }

    public async Task<ValidationResult> CreateCustomerUserAsync(CustomerUserRequest request)
    {
        var localizer = _localizerFactory.Create(typeof(CustomerUserValidator));
        var validationResult = new CustomerUserValidator(localizer).Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.Phone,
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            LockoutEnabled = true,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(e => WithError(e.Description));
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
        });

        if (!requestResult.ValidationResult.IsValid)
        {
            await _userManager.DeleteAsync(user);
            return requestResult.ValidationResult;
        }

        await _userManager.AddToRoleAsync(user, RoleContant.Customer);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _emailService.SendEmailConfirmationAccountAsync(request.Email, token);

        return ValidationResult;
    }

    public async Task<ValidationResult> ConfirmEmailAccountAsync(EmailAccountConfirmationRequest request)
    {
        var localizer = _localizerFactory.Create(typeof(EmailAccountConfirmationValidator));
        var validationResult = new EmailAccountConfirmationValidator(localizer).Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return WithError(_localizer["Cannot Confirm Email Account"]);
        }

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        return result.Succeeded ?
            ValidationResult :
            WithError(_localizer["Cannot Confirm Email Account"]);
    }

    public async Task<ValidationResult> ResendConfirmEmailAccountAsync(EmailRequest request)
    {
        var localizer = _localizerFactory.Create(typeof(EmailValidator));
        var validationResult = new EmailValidator(localizer).Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return WithError(_localizer["Cannot Resend Confirm Email Account"]);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _emailService.SendEmailConfirmationAccountAsync(user.Email, token);

        return ValidationResult;
    }

    public async Task<ValidationResult> SendResetPasswordAsync(EmailRequest request)
    {
        var localizer = _localizerFactory.Create(typeof(EmailValidator));
        var validationResult = new EmailValidator(localizer).Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
        {
            return WithError(_localizer["Cannot Send Reset Password"]);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _emailService.SendEmailResetPasswordAsync(user.Email, token);

        return ValidationResult;
    }

    public async Task<ValidationResult> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var localizer = _localizerFactory.Create(typeof(ResetPasswordValidator));
        var validationResult = new ResetPasswordValidator(localizer).Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
        {
            return WithError(_localizer["Cannot Reset Password"]);
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

        return result.Succeeded ?
            ValidationResult :
            WithError(_localizer["Cannot Reset Password"]);
    }

    private static CreateRestaurantCommand FromRequestToCommand(RestaurantUserRequest request, Guid userId)
        => new()
        {
            UserId = userId,
            Email = request.Email!,
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