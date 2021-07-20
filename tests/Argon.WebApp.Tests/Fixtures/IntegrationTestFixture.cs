using Argon.Catalog.Infra.Data;
using Argon.Customers.Infra.Data;
using Argon.Identity.Data;
using Argon.WebApp.API;
using Argon.WebApp.Tests.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Xunit;

namespace Argon.WebApp.Tests.Fixtures
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
            var services = Factory.Server.Services;
            var cusomerContext = services.GetRequiredService<CustomerContext>();
            var identityContext = services.GetRequiredService<IdentityContext>();
            var catalogContext = services.GetRequiredService<CatalogContext>();

            cusomerContext.Database.EnsureDeleted();
            identityContext.Database.EnsureDeleted();
            catalogContext.Database.EnsureDeleted();

            HttpClient?.Dispose();
            Factory?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
