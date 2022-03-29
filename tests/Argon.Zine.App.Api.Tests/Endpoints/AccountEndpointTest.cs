using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Requests;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Argon.Zine.App.Api.Tests.Endpoints;

/* endpoints ✔️❌
 - register-customer - ✔️
 - register-restaurant - ✔️
 - email-confirmation - ❌
 - resend-email-confirmation - ✔️
 - send-reset-password - ✔️
 - reset-password - ✔️
 */

[Collection(nameof(AplicationFixtureCollection))]
public class AccountEndpointTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationFixture<Startup> _applicationFixture;
    private readonly string _confirmedEmail = "confirmed_email@email.com";
    private readonly string _nonConfirmedEmail = "non_confirmed_email@email.com";
    public AccountEndpointTest(
        ITestOutputHelper testOutputHelper,
        ApplicationFixture<Startup> applicationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _applicationFixture = applicationFixture;
    }

    [Fact]
    public async Task Create_customer_account_success()
    {
        //Arrange
        var faker = new Faker("pt_BR");
        var person = faker.Person;
        var request = new CustomerUserRequest
        {
            BirthDate = person.DateOfBirth,
            Cpf = person.Cpf(),
            Email = person.Email,
            FirstName = person.FirstName,
            Surname = person.LastName,
            Password = faker.Internet.Password(prefix: "@12Ab")
        };

        //Act
        var response = await _applicationFixture.HttpClient!.PostAsJsonAsync("api/account/register-customer", request);

        //Assert
        response.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Create_customer_account_error()
    {
        //Arrange
        var faker = new Faker("pt_BR");
        var person = faker.Person;
        var request = new CustomerUserRequest
        {
            BirthDate = person.DateOfBirth,
            Cpf = "test",
            Email = person.Email,
            FirstName = person.FirstName,
            Surname = person.LastName,
            Password = faker.Internet.Password(prefix: "@12Ab")
        };

        //Act
        var response = await _applicationFixture.HttpClient!.PostAsJsonAsync("api/account/register-customer", request);

        //Assert
        response.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }


    [Fact]
    public async Task Create_restaurant_account_success()
    {
        //Arrange
        var faker = new Faker("pt_BR");
        var person = faker.Person;
        var request = new RestaurantUserRequest
        {
            City = faker.Address.City(),
            Latitude = faker.Address.Latitude(),
            Longitude = faker.Address.Longitude(),
            Number = faker.Address.BuildingNumber(),
            PostalCode = faker.Address.ZipCode(),
            State = "CE",
            Street = faker.Address.StreetName(),
            District = faker.Lorem.Letter(20),
            CpfCnpj = faker.Company.Cnpj(),
            CorparateName = faker.Company.CompanyName(),
            TradeName = faker.Company.CompanyName(),
            Email = person.Email,
            FirstName = person.FirstName,
            Surname = person.LastName,
            Password = faker.Internet.Password(prefix: "@12Ab")
        };

        //Act
        var response = await _applicationFixture.HttpClient!.PostAsJsonAsync("api/account/register-restaurant", request);

        //Assert
        response.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Create_restaurant_account_error()
    {
        //Arrange
        var faker = new Faker("pt_BR");
        var person = faker.Person;
        var request = new RestaurantUserRequest
        {
            City = faker.Address.City(),
            Latitude = faker.Address.Latitude(),
            Longitude = faker.Address.Longitude(),
            Number = faker.Address.BuildingNumber(),
            PostalCode = faker.Address.ZipCode(),
            State = "INVALID",
            Street = faker.Address.StreetName(),
            District = faker.Lorem.Letter(20),
            CpfCnpj = faker.Company.Cnpj(),
            CorparateName = faker.Company.CompanyName(),
            TradeName = faker.Company.CompanyName(),
            Email = person.Email,
            FirstName = person.FirstName,
            Surname = person.LastName,
            Password = faker.Internet.Password(prefix: "@12Ab")
        };

        //Act
        var response = await _applicationFixture.HttpClient!.PostAsJsonAsync("api/account/register-restaurant", request);

        //Assert
        response.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Resend_email_confirmation_success()
    {
        //Arrange
        var email = new Faker("pt_BR").Person.Email;
        var emailRequest = new EmailRequest { Email = _nonConfirmedEmail };

        //Act
        var resendEmailConfirmationResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/resend-email-confirmation", emailRequest);

        //Assert
        resendEmailConfirmationResponse.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Resend_email_confirmation_error()
    {
        //Arrange
        var emailRequest = new EmailRequest { Email = "notfound@email.com" };

        //Act
        var resendEmailConfirmationResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/resend-email-confirmation", emailRequest);

        //Assert
        resendEmailConfirmationResponse.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Send_reset_password_success()
    {
        //Arrange
        var emailRequest = new EmailRequest { Email = _confirmedEmail };

        //Act
        var sendResetPasswordResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/send-reset-password", emailRequest);

        //Assert
        sendResetPasswordResponse.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Send_reset_password_error()
    {
        //Arrange
        var emailRequest = new EmailRequest { Email = "notfound@email.com" };

        //Act
        var sendResetPasswordResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/send-reset-password", emailRequest);

        //Assert
        sendResetPasswordResponse.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Reset_password_success()
    {
        //Arrange
        var getPasswordResetToken = async () =>
        {
            var userManager = _applicationFixture.Factory!.Services.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(_confirmedEmail);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        };

        //Act
        var sendResetPasswordResponse = await _applicationFixture.HttpClient!.PutAsJsonAsync(
            "api/account/reset-password", new ResetPasswordRequest
            {
                Email = _confirmedEmail,
                Password = "P@$$wOrd999",
                Token = await getPasswordResetToken()
            });

        //Assert
        sendResetPasswordResponse.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Reset_password_error()
    {
        //Arrange
        var getPasswordResetToken = async () =>
        {
            var userManager = _applicationFixture.Factory!.Services.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(_confirmedEmail);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        };

        //Act
        var sendResetPasswordResponse = await _applicationFixture.HttpClient!.PutAsJsonAsync(
            "api/account/reset-password", new ResetPasswordRequest
            {
                Email = "notfound@email.com",
                Password = "P@$$wOrd999",
                Token = await getPasswordResetToken()
            });

        //Assert
        sendResetPasswordResponse.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }
}