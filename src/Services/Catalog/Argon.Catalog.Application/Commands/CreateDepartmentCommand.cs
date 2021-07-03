using Argon.Core.Messages;

namespace Argon.Catalog.Application.Commands
{
    public record CreateDepartmentCommand : Command
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
    }
}
