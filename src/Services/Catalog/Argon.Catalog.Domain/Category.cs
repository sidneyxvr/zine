using Argon.Core.DomainObjects;

namespace Argon.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public Category(string name)
        {
            Name = name;
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
