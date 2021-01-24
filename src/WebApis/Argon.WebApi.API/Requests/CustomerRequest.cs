using Argon.Core.DomainObjects;
using System;

namespace Argon.WebApi.API.Requests
{
    public class CustomerRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
