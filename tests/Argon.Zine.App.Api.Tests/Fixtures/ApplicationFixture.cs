using System.Net.Http;

namespace Argon.Zine.App.Api.Tests.Fixtures;

[CollectionDefinition(nameof(AplicationFixtureCollection))]
public class AplicationFixtureCollection : ICollectionFixture<ApplicationFixture<Startup>> { }

public class ApplicationFixture<TStartup> : IDisposable where TStartup : class
{
    public readonly HttpClient? HttpClient;
    public readonly CustomWebApplicationFactory<TStartup>? Factory;

    public ApplicationFixture()
    {
        Factory = new CustomWebApplicationFactory<TStartup>();
        HttpClient = Factory.CreateClient();
        HttpClient.Timeout = TimeSpan.FromSeconds(5);
        HttpClient.DefaultRequestHeaders.Add("origin", "http://localhost");
    }

    public void Dispose()
    {
    }

    public void SetAuthorizationHeader(string token)
    {
        HttpClient!.DefaultRequestHeaders.Add("authorization", $"Bearer {token}");
    }
}