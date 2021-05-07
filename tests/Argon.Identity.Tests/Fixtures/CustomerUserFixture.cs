using Argon.Core.DomainObjects;
using Bogus;
using Bogus.Extensions.Brazil;
using System;

namespace Argon.Identity.Tests.Fixtures
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
            var LastName = _faker.Person.LastName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf();
            var birthDate = DateTime.UtcNow.AddYears(-20);
            var gender = _faker.Random.Enum<Gender>();
            var password = _faker.Internet.Password();

            return new CustomerUser(firstName, LastName, email, cpf, birthDate, gender, password);
        }
    }

    public record CustomerUser(string FirstName, string LastName, string Email,
        string Cpf, DateTime BirthDate, Gender Gender, string Password);
}
