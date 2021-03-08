using Argon.Identity.Requests;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Argon.Identity.Services
{
    public interface IAccountService
    {
        Task<ValidationResult> SendResetPasswordAsync(EmailRequest request);
        Task<ValidationResult> ResetPasswordAsync(ResetPasswordRequest request);
        Task<ValidationResult> ResendConfirmAccountEmailAsync(EmailRequest request);
        Task<ValidationResult> CreateCustomerUserAsync(CustomerUserRequest request);
        Task<ValidationResult> ConfirmAccountEmailAsync(AccountEmailConfirmationRequest request);
        Task<ValidationResult> UpdateTwoFactorAuthenticationAsync(UpdateTwoFactorAuthenticationRequest request);
    }
}
