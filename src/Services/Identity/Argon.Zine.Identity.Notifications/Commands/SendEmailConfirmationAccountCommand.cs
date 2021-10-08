namespace Argon.Zine.Identity.Notifications.Commands;

internal record SendEmailConfirmationAccountCommand : ICommand
{
    public string Email { get; set; } = null!;
}