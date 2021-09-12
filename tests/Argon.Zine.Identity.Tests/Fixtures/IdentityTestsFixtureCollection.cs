using Bogus;
using Microsoft.Extensions.Localization;
using Moq.AutoMock;
using Xunit;

namespace Argon.Identity.Tests.Fixture
{
    [CollectionDefinition(nameof(IdentityTestsFixtureCollection))]
    public class IdentityTestsFixtureCollection : ICollectionFixture<IdentityTestsFixture> { }

    public class IdentityTestsFixture
    {
        public readonly Faker Faker;
        public readonly AutoMocker Mocker;

        public IdentityTestsFixture()
        {
            Faker = new("pt_BR");
            Mocker = new AutoMocker();
            Mocker.Use<IStringLocalizerFactory>(new StringLocalizerFactory());
        }
    }

    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            var t = LocalizerHelper.CreateInstanceStringLocalizer(resourceSource);
            return t;
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
