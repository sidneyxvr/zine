using Argon.Zine.Catalog.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit.Abstractions;

namespace Argon.Zine.App.Api.Tests.Endpoints;

[Collection(nameof(AplicationFixtureCollection))]
public class CategoriesEndpointTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationFixture<Startup> _applicationFixture;

    public CategoriesEndpointTest(
        ITestOutputHelper testOutputHelper,
        ApplicationFixture<Startup> applicationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _applicationFixture = applicationFixture;
    }

    [Fact]
    public async Task Create_category_should_return_ok()
    {
        //Arrange
        var faker = new Faker("pt_BR");
        var request = new CreateCategoryCommand(faker.Lorem.Letter(20), faker.Lorem.Letter(20));
        await _applicationFixture.SingInOrSetTokenAsync();

        //Act
        var response = await _applicationFixture.HttpClient!.PostAsJsonAsync("api/categories", request);

        //Assert
        response.Should().BeHttpResponseOkOrLogError(_testOutputHelper.WriteLine);
    }

    [Fact]
    public async Task Create_category_should_return_error()
    {
        //Arrange
        var faker = new Faker("pt_BR");
        var request = new CreateCategoryCommand("categoria", faker.Lorem.Letter(20));
        await _applicationFixture.SingInOrSetTokenAsync();

        //Act
        var response = await _applicationFixture.HttpClient!.PostAsJsonAsync("api/categories", request);
        var validationProblemDetails = (await response.Content.ReadFromJsonAsync<ValidationProblemDetails>())!;

        //Assert
        response.Should().BeHttpResponseBadRequestOrLogError(_testOutputHelper.WriteLine);
        validationProblemDetails.Errors.Should().ContainKey("category");
    }
}