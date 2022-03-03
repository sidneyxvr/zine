using Argon.Zine.Identity.Notifications.Commands;
using FluentEmail.Core;

namespace Argon.Zine.Identity.Notifications.Handlers;

internal class SendEmailConfirmationAccountHandler : IHandler<SendEmailConfirmationAccountCommand>
{
    private readonly IFluentEmail _emailSender;
    private static readonly string s_path =
        Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ConfirmationAccount.cshtml");

    public SendEmailConfirmationAccountHandler(IFluentEmail emailSender)
        => _emailSender = emailSender;

    public async Task HandleAsync(SendEmailConfirmationAccountCommand command)
        => await _emailSender
            .To(command.Email)
            .Subject("") //_localizer.GetTranslation("ConfirmationAccountSubject"))
            .UsingTemplateFromFile(s_path, command)
            .SendAsync();
}