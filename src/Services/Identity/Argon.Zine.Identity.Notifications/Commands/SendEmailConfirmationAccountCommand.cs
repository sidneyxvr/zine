namespace Argon.Zine.Identity.Notifications.Commands;

internal record SendEmailConfirmationAccountCommand(string Email) : ICommand;
