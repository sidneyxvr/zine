using Argon.Zine.Identity.Notifications.Commands;
using FluentEmail.Core;

namespace Argon.Zine.Identity.Notifications.Handlers;

public class SendEmailResetPasswordHandler
{
    private readonly IFluentEmail _emailSender;

    public SendEmailResetPasswordHandler(IFluentEmail emailSender)
        => _emailSender = emailSender;

    public async Task HandleAsync(SendEmailResetPasswordCommand command)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ResetPassword.cshtml");

        await _emailSender
            .To(command.Email)
            .Subject("Zine - Recuperar Senha")//_localizer.GetTranslation("ConfirmationAccountSubject"))
            .UsingTemplateFromFile(path, new { command.Token })
            .SendAsync();
    }
}