using Bogus;

namespace Argon.Identity.Test.Fixtures
{
    public class CustomerUserFixture
    {
        private readonly Faker _faker;

        public CustomerUserFixture()
        {
            _faker = new Faker("pr_BR");
        }
    }
}
