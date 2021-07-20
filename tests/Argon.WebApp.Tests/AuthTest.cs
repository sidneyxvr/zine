using Argon.Identity.Requests;
using Argon.Identity.Responses;
using Argon.WebApp.API;
using Argon.WebApp.Tests.Fixtures;
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

        [Fact]
        public async Task Auth_RefreshToken_ShouldReturnAccessToken()
        {
            //Arrage
            var request = new LoginRequest { Email = "teste@email.com", Password = "Teste@123" };

            //Act
            var response = await _apiFixture.HttpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<UserLoginResponse>();

            var request2 = new RefreshTokenRequest { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };
            var response2 = await _apiFixture.HttpClient.PostAsJsonAsync("api/auth/refresh-token", request2);
            var result2 = await response2.Content.ReadFromJsonAsync<UserLoginResponse>();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotEqual(0, result2.ExpiresIn);
            Assert.NotNull(result2.UserToken);
            Assert.NotNull(result2.AccessToken);
            Assert.NotNull(result2.RefreshToken);
        }

        [Fact]
        public async Task Auth_RefreshToken_ShouldReturnBadRequest()
        {
            //Arrage
            var request = new LoginRequest { Email = "teste@email.com", Password = "Teste@123" };

            //Act
            var response = await _apiFixture.HttpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<UserLoginResponse>();

            var request2 = new RefreshTokenRequest { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken += "test" };
            var response2 = await _apiFixture.HttpClient.PostAsJsonAsync("api/auth/refresh-token", request2);
            var result2 = await response2.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            //Assert
            Assert.Equal(400, result2.Status);
            Assert.Contains(result2.Errors.Values, e => e.Any(v => v.Equals("Não foi possível atualizar o token de acesso")));
        }
    }
}
