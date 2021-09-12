using System;

namespace Argon.Customers.Application.Reponses
{
    public record CustomerReponse
    {
        public string FirstName { get; init; } = null!;
        public string? LastName { get; init; }
        public string Email { get; init; } = null!;
        public string Cpf { get; init; } = null!;
        public DateTime BirthDate { get; init; }
        public string? Phone { get; init; }
        public int Gender { get; init; }
        public bool IsActive { get; init; }
        public bool IsDeleted { get; init; }
        public bool IsSuspended { get; init; }
        public AddressReponse? MainAddress { get; init; }
    }
}
