namespace Argon.Zine.Identity.Notifications.Commands;

public class SendEmailResetPasswordCommand
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}