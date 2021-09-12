using System;

namespace Argon.Zine.Customers.Application.Reponses
{
    public record CustomerNameResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }
}
