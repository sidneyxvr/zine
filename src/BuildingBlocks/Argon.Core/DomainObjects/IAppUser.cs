using System;

namespace Argon.Core.DomainObjects
{
    public interface IAppUser
    {
        Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string FullName { get; }
    }
}
