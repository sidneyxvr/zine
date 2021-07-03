using Argon.Core.DomainObjects;
using Argon.Core.Utils;

namespace Argon.Identity.Requests
{
    public record CustomerUserRequest : BaseRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        private string? _cpf;
        public string? Cpf { get => _cpf?.OnlyNumbers(); init => _cpf = value; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? Password { get; set; }
    }
}
