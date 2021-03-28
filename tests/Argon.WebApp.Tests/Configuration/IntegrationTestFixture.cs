using Argon.WebApp.API;
using System;
using System.Net.Http;
using Xunit;

namespace Argon.WebApp.Tests.Configuration
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Startup>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient HttpClient;
        public readonly ArgonAppFactory<TStartup> Factory;

        public IntegrationTestsFixture()
        {
            Factory = new ArgonAppFactory<TStartup>();
            HttpClient = Factory.CreateClient();
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
            Factory?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
