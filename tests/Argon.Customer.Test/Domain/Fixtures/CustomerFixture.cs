using Argon.Core.DomainObjects;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Bogus;
using Bogus.Extensions.Brazil;
using System;

namespace Argon.Customers.Test.Domain.Fixtures
{
    public class CustomerFixture
    {
        private readonly Faker _faker;
        public CustomerFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public (string FullName, string Email, string Cpf, DateTime BirthDate, string Phone, Gender Gender) GetValidCustomerProperties()
        {
            var fullName = _faker.Person.FullName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf(false);
            var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
            var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";
            var gender = _faker.PickRandom<Gender>();

            return (fullName, email, cpf, birthDate, phone, gender);
        }

        public Customer CreateValidCustomer()
        {
            var fullName = _faker.Person.FullName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf(false);
            var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
            var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";
            var gender = _faker.PickRandom<Gender>();

            var customer = new Customer(Guid.NewGuid(), fullName, email, cpf, birthDate, gender, phone);

            return customer;
        }
    }
}
