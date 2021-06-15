using Argon.Core.Internationalization;
using Argon.Identity.Services;
using FluentEmail.Core;
using System.IO;
using System.Threading.Tasks;

namespace Argon.WebApp.API.TemplateEmails
{
    public class IdentityEmailService : IEmailService
    {
        private readonly IFluentEmail _emailSender;
        private readonly Localizer _localizer;
        private readonly bool _sendEmail;

        private const string _folderBase = "EmailTemplates";

        public IdentityEmailService(IFluentEmail emailSender)
        {
            _emailSender = emailSender;
            _localizer = Localizer.GetLocalizer();
            _sendEmail = false;
        }

        public async Task SendEmailConfirmationAccountAsync(string to, string emailConfirmationToken)
        {
            if (!_sendEmail) return;

            if (string.IsNullOrWhiteSpace(to) ||
                !Core.DomainObjects.Email.IsValid(to) ||
                string.IsNullOrWhiteSpace(emailConfirmationToken))
            {
                return;
            }

            var model = new { Token = emailConfirmationToken };

            var path = Path.Combine(Directory.GetCurrentDirectory(), _folderBase, "ConfirmationAccount.cshtml");

            await _emailSender
                .To(to)
                .Subject(_localizer.GetTranslation("ConfirmationAccountSubject"))
                .UsingTemplateFromFile(path, model)
                .SendAsync();
        }

        public async Task SendEmailResetPasswordAsync(string to, string resetPasswordToken)
        {
            if (!_sendEmail) return;

            if (string.IsNullOrWhiteSpace(to) ||
                !Core.DomainObjects.Email.IsValid(to) ||
                string.IsNullOrWhiteSpace(resetPasswordToken))
            {
                return;
            }

            var model = new { Token = resetPasswordToken };

            var path = Path.Combine(Directory.GetCurrentDirectory(), _folderBase, "ResetPassword.cshtml");

            await _emailSender
                .To(to)
                .Subject(_localizer.GetTranslation("ResetPasswordSubject"))
                .UsingTemplateFromFile(path, model)
                .SendAsync();
        }

        public async Task SendEmailTwoFactorAuthenticationAsync(string to, string twoFactorAuthenticationCode)
        {
            if (!_sendEmail) return;

            var model = new { Code = twoFactorAuthenticationCode };

            var path = Path.Combine(Directory.GetCurrentDirectory(), _folderBase, "TwoFactorAuthentication.cshtml");

            await _emailSender
                .To(to)
                .Subject(_localizer.GetTranslation("TwoFactorAuthenticationSubject"))
                .UsingTemplateFromFile(path, model)
                .SendAsync();
        }
    }
}
