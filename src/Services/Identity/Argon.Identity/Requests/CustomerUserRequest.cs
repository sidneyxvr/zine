using Argon.Core.DomainObjects;
using Argon.Core.Utils;

namespace Argon.Identity.Requests
{
    public record CustomerUserRequest : BaseRequest
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        private string? _cpf;
        public string? Cpf { get => _cpf?.OnlyNumbers(); init => _cpf = value; }
        public DateTime BirthDate { get; init; }
        public Gender Gender { get; init; }
        public string? Password { get; init; }
    }
}
