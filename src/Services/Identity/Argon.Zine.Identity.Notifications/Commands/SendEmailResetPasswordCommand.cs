namespace Argon.Zine.Identity.Notifications.Commands;

internal record SendEmailResetPasswordCommand(string Email, string Token) : ICommand;