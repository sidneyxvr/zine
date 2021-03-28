using Argon.Identity.Requests;
using Argon.Identity.Responses;
using Argon.WebApp.API;
using Argon.WebApp.Tests.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Argon.WebApp.Tests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AuthTest
    {
        private readonly IntegrationTestsFixture<Startup> _apiFixture;

        public AuthTest(IntegrationTestsFixture<Startup> apiFixture)
        {
            _apiFixture = apiFixture;
        }

        [Fact]
        public async Task Auth_Login_ShouldReturnAccessToken()
        {
            //Arrage
            var request = new LoginRequest { Email = "teste@email.com", Password = "Teste@123" };

            //Act
            var response = await _apiFixture.HttpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<UserLoginResponse>();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotEqual(0, result.ExpiresIn);
            Assert.NotNull(result.UserToken);
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.RefreshToken);
        }

        [Fact]
        public async Task Auth_Login_ShouldReturnBadRequest()
        {
            //Arrage
            var request = new LoginRequest { Email = "teste@email.com", Password = "teste" };

            //Act
            var response = await _apiFixture.HttpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            //Assert
            Assert.Equal(400, result.Status);
            Assert.Contains(result.Errors.Values, e => e.Any(v => v.Equals("Email ou senha inválido(s)")));
        }
    }
}
