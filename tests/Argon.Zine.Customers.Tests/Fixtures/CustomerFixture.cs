using Argon.Zine.Customers.Domain;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Linq;

namespace Argon.Zine.Customers.Tests.Fixtures;

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
        var Surname = _faker.Person.LastName;
        var email = _faker.Person.Email;
        var cpf = _faker.Person.Cpf(false);
        var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
        var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";

        return new CustomerTestDTO(firstName, Surname, email, cpf, birthDate, phone);
    }

    public Customer CreateValidCustomer()
    {
        var firstName = _faker.Person.FirstName;
        var Surname = _faker.Person.LastName;
        var email = _faker.Person.Email;
        var cpf = _faker.Person.Cpf(false);
        var birthDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(18, 99)).AddSeconds(-2);
        var phone = $"{_faker.Random.Int(1, 9)}{_faker.Random.Int(1, 9)}{_faker.Random.Int(910000000, 999999999)}";

        return new Customer(Guid.NewGuid(), new(firstName, Surname), email, cpf, birthDate, phone);
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

public record CustomerTestDTO(string FirstName, string Surname, string Email, string Cpf, DateTime BirthDate, string Phone);