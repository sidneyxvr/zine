using System.Threading.Tasks;

namespace Argon.Identity.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAccountAsync(string to, string emailConfirmationToken);
        Task SendEmailResetPasswordAsync(string to, string resetPasswordToken);
        Task SendEmailTwoFactorAuthenticationAsync(string to, string twoFactorAuthenticationCode);
    }
}
