using Bogus;
using Microsoft.Extensions.Localization;
using Moq.AutoMock;
using Xunit;

namespace Argon.Zine.Identity.Tests.Fixture
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
            => LocalizerHelper.CreateInstanceStringLocalizer(resourceSource);

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
