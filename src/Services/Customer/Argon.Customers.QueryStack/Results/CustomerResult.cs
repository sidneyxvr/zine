using System;

namespace Argon.Customers.QueryStack.Results
{
    public class CustomerResult
    {
        public string FirstName { get; init; }
        public string LastName { get; set; }
        public string Email { get; init; }
        public string Cpf { get; init; }
        public DateTime BirthDate { get; init; }
        public string Phone { get; init; }
        public int Gender { get; init; }
        public bool IsActive { get; init; }
        public bool IsDeleted { get; init; }
        public bool IsSuspended { get; init; }
        public AddressResult MainAddress { get; init; }
    }
}
