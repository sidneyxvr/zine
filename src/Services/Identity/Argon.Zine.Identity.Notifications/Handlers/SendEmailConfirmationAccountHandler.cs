using Argon.Zine.Identity.Notifications.Commands;
using FluentEmail.Core;

namespace Argon.Zine.Identity.Notifications.Handlers;

public class SendEmailConfirmationAccountHandler
{
    private readonly IFluentEmail _emailSender;

    public SendEmailConfirmationAccountHandler(IFluentEmail emailSender) 
        => _emailSender = emailSender;

    public async Task HandleAsync(SendEmailConfirmationAccountCommand command)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ConfirmationAccount.cshtml");

        await _emailSender
            .To(command.Email)
            .Subject("")//_localizer.GetTranslation("ConfirmationAccountSubject"))
            .UsingTemplateFromFile(path, command)
            .SendAsync();
    }
}
