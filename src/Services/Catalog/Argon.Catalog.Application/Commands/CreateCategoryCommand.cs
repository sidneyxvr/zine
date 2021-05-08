using Argon.Core.Messages;

namespace Argon.Catalog.Application.Commands
{
    public class CreateCategoryCommand : Command
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
