using Argon.Zine.Identity.Notifications.Commands;
using FluentEmail.Core;

namespace Argon.Zine.Identity.Notifications.Handlers;

internal class SendEmailResetPasswordHandler : IHandler<SendEmailResetPasswordCommand>
{
    private readonly IFluentEmail _emailSender;

    public SendEmailResetPasswordHandler(IFluentEmail emailSender)
        => _emailSender = emailSender;

    public async Task HandleAsync(SendEmailResetPasswordCommand command)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ResetPassword.cshtml");

        await _emailSender
            .To(command.Email)
            .Subject("Zine - Recuperar Senha")
            .UsingTemplateFromFile(path, new { command.Token })
            .SendAsync();
    }
}