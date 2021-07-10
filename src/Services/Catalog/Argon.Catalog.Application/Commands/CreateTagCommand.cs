using Argon.Core.Messages;

namespace Argon.Catalog.Application.Commands
{
    public record CreateTagCommand : Command
    {
        public string? Name { get; init; }
    }
}
