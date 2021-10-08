using Argon.Zine.Identity.Notifications.Commands;

namespace Argon.Zine.Identity.Notifications.Handlers;

internal interface IHandler<in TCommand> 
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}