using Argon.Core.DomainObjects;
using Argon.Identity.Requests;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using Xunit;

namespace Argon.WebApp.Tests.Fixtures
{
    [CollectionDefinition(nameof(AccountCollection))]
    public class AccountCollection : ICollectionFixture<AccountFixture> { }
    public class AccountFixture
    {
        private readonly Faker _faker;

        public AccountFixture()
        {
            _faker = new Faker("pt_BR");
        }

        public CustomerUserRequest CreateValidCustomerUserRequest()
            => new()
            {
                BirthDate = DateTime.UtcNow.AddYears(_faker.Random.Int(-90, -19)),
                Cpf = _faker.Person.Cpf(),
                Email = _faker.Person.Email,
                FirstName = _faker.Person.FirstName,
                Surname = _faker.Person.LastName,
                Gender = _faker.PickRandom<Gender>(),
                Password = _faker.Internet.Password()
            };

        public CustomerUserRequest CreateInvalidCustomerUserRequest()
            => new()
            {
                BirthDate = DateTime.UtcNow.AddYears(_faker.Random.Int(-90, -19)),
                Cpf = _faker.Person.Email,
                Email = _faker.Person.Email,
                FirstName = _faker.Person.FirstName,
                Surname = _faker.Person.LastName,
                Gender = _faker.PickRandom<Gender>(),
                Password = _faker.Internet.Password()
            };
    }
}
