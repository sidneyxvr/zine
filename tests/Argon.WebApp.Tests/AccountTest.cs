using Argon.WebApp.API;
using Argon.WebApp.Tests.Configuration;
using Argon.WebApp.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Argon.WebApp.Tests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AccountTest
    {
        private readonly AccountFixture _accountFixture;
        private readonly IntegrationTestsFixture<Startup> _apiFixture;

        public AccountTest(IntegrationTestsFixture<Startup> apiFixture)
        {
            _apiFixture = apiFixture;
            _accountFixture = new AccountFixture();
        }

        [Fact]
        public async Task Account_RegisterCustomer_ShouldReturnOk()
        {
            //Arrange
            var request = _accountFixture.CreateValidCustomerUserRequest();

            //Act
            var response = await _apiFixture.HttpClient.PostAsJsonAsync("api/account/register-customer", request);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async Task Account_CreateUser_ShouldReturnBadRequest()
        {
            //Arrange
            var request = _accountFixture.CreateInvalidCustomerUserRequest();

            //Act
            var response = await _apiFixture.HttpClient.PostAsJsonAsync("api/account/register-customer", request);
            var result = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            //Assert
            Assert.Equal(400, result.Status);
            Assert.NotEqual(0, result.Errors.Count);
        }
    }
}
