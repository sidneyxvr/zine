using Argon.Zine.Commom.Messages;

namespace Argon.Zine.Catalog.Application.Commands;

public record CreateCategoryCommand(string Name, string Description) : Command;