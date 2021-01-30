using Argon.Core.DomainObjects;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;

namespace Argon.Customers.Test.Domain.Fixtures
{
    public class CustomerFixture
    {
        private readonly Faker _faker;
        public CustomerFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public CustomerTestDTO GetCustomerTestDTO()
        {
            var firstName = _faker.Person.FirstName;
            var surname = _faker.Person.LastName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf(false);
            var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
            var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";
            var gender = _faker.PickRandom<Gender>();

            return new CustomerTestDTO(firstName, surname, email, cpf, birthDate, phone, gender);
        }

        public Customer CreateValidCustomer()
        {
            var firstName = _faker.Person.FirstName;
            var surname = _faker.Person.LastName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf(false);
            var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
            var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";
            var gender = _faker.PickRandom<Gender>();

            return new Customer(Guid.NewGuid(), firstName, surname, email, cpf, birthDate, gender, phone);
        }
    }

    public record CustomerTestDTO(string FirstName, string Surname, string Email, string Cpf, DateTime BirthDate, string Phone, Gender Gender);
}
