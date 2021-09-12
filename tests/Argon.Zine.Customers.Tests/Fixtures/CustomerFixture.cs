using Argon.Zine.Core.DomainObjects;
using Argon.Customers.Domain;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Linq;

namespace Argon.Customers.Tests.Fixtures
{
    public class CustomerFixture
    {
        private readonly Faker _faker;
        private readonly AddressFixture _addressFixture;
        public CustomerFixture()
        {
            _faker = new Faker("pt_BR");
            _addressFixture = new AddressFixture();
        }

        public CustomerTestDTO GetCustomerTestDTO()
        {
            var firstName = _faker.Person.FirstName;
            var LastName = _faker.Person.LastName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf(false);
            var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
            var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";
            var gender = _faker.PickRandom<Gender>();

            return new CustomerTestDTO(firstName, LastName, email, cpf, birthDate, phone, gender);
        }

        public Customer CreateValidCustomer()
        {
            var firstName = _faker.Person.FirstName;
            var LastName = _faker.Person.LastName;
            var email = _faker.Person.Email;
            var cpf = _faker.Person.Cpf(false);
            var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
            var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";
            var gender = _faker.PickRandom<Gender>();

            return new Customer(Guid.NewGuid(), firstName, LastName, email, cpf, birthDate, gender, phone);
        }

        public Customer CreateValidCustomerWithAddresses()
        {
            var customer = CreateValidCustomer();

            var addresses = Enumerable
                .Range(1, _faker.Random.Int(1, 5))
                .Select(_ => _addressFixture.CreateValidAddress())
                .ToList();

            var mainAddress = addresses.First();

            addresses.ForEach(address => customer.AddAddress(address));
            customer.DefineMainAddress(mainAddress.Id);

            return customer;
        }
    }

    public record CustomerTestDTO(string FirstName, string LastName, string Email, string Cpf, DateTime BirthDate, string Phone, Gender Gender);
}
