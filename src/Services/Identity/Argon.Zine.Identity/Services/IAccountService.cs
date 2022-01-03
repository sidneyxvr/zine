using Argon.Zine.Identity.Requests;
using FluentValidation.Results;

namespace Argon.Zine.Identity.Services;

public interface IAccountService
{
    Task<ValidationResult> SendResetPasswordAsync(EmailRequest request);
    Task<ValidationResult> ResetPasswordAsync(ResetPasswordRequest request);
    Task<ValidationResult> ResendConfirmEmailAccountAsync(EmailRequest request);
    Task<ValidationResult> CreateCustomerUserAsync(CustomerUserRequest request);
    Task<ValidationResult> CreateRestaurantUserAsync(RestaurantUserRequest request);
    Task<ValidationResult> ConfirmEmailAccountAsync(EmailAccountConfirmationRequest request);
}