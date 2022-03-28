using Argon.Zine.Identity.Responses;
using System.Net.Http;

namespace Argon.Zine.App.Api.Tests.Fixtures;

[CollectionDefinition(nameof(AplicationFixtureCollection))]
public class AplicationFixtureCollection : ICollectionFixture<ApplicationFixture<Startup>> { }

public class ApplicationFixture<TStartup> : IDisposable where TStartup : class
{
    public readonly HttpClient HttpClient;
    public readonly CustomWebApplicationFactory<TStartup> Factory;
    
    public ApplicationFixture()
    {
        Factory = new CustomWebApplicationFactory<TStartup>();
        HttpClient = Factory.CreateClient();
        HttpClient.Timeout = TimeSpan.FromSeconds(5);
        HttpClient.DefaultRequestHeaders.Add("origin", "http://localhost");
    }

    public void Dispose()
    {
        Factory.Dispose();
        HttpClient.Dispose();
    }

    public async Task SingInOrSetTokenAsync()
    {
        lock (HttpClient)
        {
            if (HttpClient.DefaultRequestHeaders.Contains("authorization"))
            {
                return;
            }
        }

        var request = new { email = "user-test@email.com", password = "Teste@123" };
        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", request);

        var result = await response.Content.ReadFromJsonAsync<UserLoginResponse>();

        HttpClient!.DefaultRequestHeaders.Add("authorization", $"Bearer {result!.AccessToken}");
    }
}