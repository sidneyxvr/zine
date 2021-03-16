using Argon.Core.DomainObjects;

namespace Argon.Suppliers.Domain
{
    public class User : Entity
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Gender Gender { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDelete { get; private set; }
    }
}
