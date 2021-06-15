using Argon.Core.Messages;
using System;

namespace Argon.Catalog.Application.Commands
{
    public class CreateCategoryCommand : Command
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Guid DepartmentId { get; init; }
    }
}
