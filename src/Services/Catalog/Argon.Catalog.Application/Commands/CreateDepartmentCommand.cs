using Argon.Core.Messages;

namespace Argon.Catalog.Application.Commands
{
    public class CreateDepartmentCommand : Command
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
