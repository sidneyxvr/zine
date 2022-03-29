using Bogus;
using Bogus.Extensions.Brazil;
using System;

namespace Argon.Zine.Identity.Tests.Fixtures
{
    public class CustomerUserFixture
    {
        private readonly Faker _faker;

        public CustomerUserFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public CustomerUser CreateCustomerUser()
        {
            var firstName = _faker.Person.FirstName;
            var Surname = _faker.Person.LastName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf();
            var birthDate = DateTime.UtcNow.AddYears(-20);
            var password = _faker.Internet.Password();

            return new CustomerUser(firstName, Surname, email, cpf, birthDate, password);
        }
    }

    public record CustomerUser(string FirstName, string Surname, string Email,
        string Cpf, DateTime BirthDate, string Password);
}
