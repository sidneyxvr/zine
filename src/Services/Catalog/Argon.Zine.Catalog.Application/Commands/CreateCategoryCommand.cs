using Argon.Zine.Core.Messages;

namespace Argon.Zine.Catalog.Application.Commands
{
    public record CreateCategoryCommand : Command
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
    }
}
