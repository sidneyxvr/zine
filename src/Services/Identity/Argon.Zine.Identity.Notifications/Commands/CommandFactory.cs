using System.Text.Json;

namespace Argon.Zine.Identity.Notifications.Commands
{
    internal static class CommandFactory
    {
        public static ICommand Create(string type, string message)
            => type switch 
            {
                string value when value.Equals("SendEmailConfirmationAccount", StringComparison.OrdinalIgnoreCase) 
                    => JsonSerializer.Deserialize<SendEmailConfirmationAccountCommand>(message)!,
                string value when value.Equals("SendEmailResetPassword", StringComparison.OrdinalIgnoreCase) 
                    => JsonSerializer.Deserialize<SendEmailResetPasswordCommand>(message)!,
                _ => throw new ArgumentException(type)
            };

    }
}
