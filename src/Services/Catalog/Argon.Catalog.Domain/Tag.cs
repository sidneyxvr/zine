using Argon.Core.DomainObjects;
using System.Collections.Generic;

namespace Argon.Catalog.Domain
{
    public class Tag : ValueObject
    {
        public string Name { get; private set; }

        public Tag(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
