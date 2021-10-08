namespace Argon.Zine.Identity.Notifications.Commands;

internal record SendEmailResetPasswordCommand : ICommand
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}