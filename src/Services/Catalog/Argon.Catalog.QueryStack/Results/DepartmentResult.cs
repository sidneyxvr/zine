using System;

namespace Argon.Catalog.QueryStack.Results
{
    public record DepartmentResult
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
