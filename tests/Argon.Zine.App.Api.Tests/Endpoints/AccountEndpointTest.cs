using Argon.Zine.Identity.Models;
using Argon.Zine.Identity.Requests;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit.Abstractions;

namespace Argon.Zine.App.Api.Tests.Endpoints;

/* endpoints ✔️❌
 - register-customer - ✔️
 - register-restaurant - ✔️
 - email-confirmation - ✔️
 - resend-email-confirmation - ✔️
 - send-reset-password - ✔️
 - reset-password - ✔️
 */

[Collection(nameof(AplicationFixtureCollection))]
public class AccountEndpointTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationFixture<Startup> _applicationFixture;

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
            LastName = person.LastName,
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
            LastName = person.LastName,
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
            LastName = person.LastName,
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
            LastName = person.LastName,
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
        var emailRequest = new EmailRequest { Email = email };

        //Act
        await Create_user_endpoint(email);

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
        await Create_user_endpoint();

        var resendEmailConfirmationResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/resend-email-confirmation", emailRequest);

        //Assert
        resendEmailConfirmationResponse.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Send_reset_password_success()
    {
        //Arrange
        var email = new Faker("pt_BR").Person.Email;
        var emailRequest = new EmailRequest { Email = email };

        //Act
        await Create_user_endpoint(email);
        await Confirm_user_email(email);

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
        await Create_user_endpoint();

        var sendResetPasswordResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/send-reset-password", emailRequest);

        //Assert
        sendResetPasswordResponse.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Reset_password_success()
    {
        //Arrange
        var email = new Faker("pt_BR").Person.Email;

        var getPasswordResetToken = async () =>
        {
            var userManager = _applicationFixture.Factory!.Services.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        };

        //Act
        await Create_user_endpoint(email);
        await Confirm_user_email(email);

        var sendResetPasswordResponse = await _applicationFixture.HttpClient!.PutAsJsonAsync(
            "api/account/reset-password", new ResetPasswordRequest
            {
                Email = email,
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
        var email = new Faker("pt_BR").Person.Email;

        var getPasswordResetToken = async () =>
        {
            var userManager = _applicationFixture.Factory!.Services.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        };

        //Act
        await Create_user_endpoint(email);
        await Confirm_user_email(email);

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

    private async Task Confirm_user_email(string email)
    {
        var getEmailConfirmationToken = async () =>
        {
            var userManager = _applicationFixture.Factory!.Services.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        };

        var emailConfirmationResponse = await _applicationFixture.HttpClient!.PostAsJsonAsync(
            "api/account/email-confirmation",
            new EmailAccountConfirmationRequest { Email = email, Token = await getEmailConfirmationToken() });

        emailConfirmationResponse.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    private async Task Create_user_endpoint(string? email = null)
    {
        var faker = new Faker("pt_BR");
        var person = faker.Person;
        var request = new CustomerUserRequest
        {
            BirthDate = person.DateOfBirth,
            Cpf = person.Cpf(),
            Email = email ?? person.Email,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Password = faker.Internet.Password(prefix: "@12Ab")
        };

        var createAccountResponse = await _applicationFixture.HttpClient!
            .PostAsJsonAsync("api/account/register-customer", request);

        createAccountResponse.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }
}