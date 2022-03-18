using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Application.Handlers;
using Argon.Zine.Catalog.Domain;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Argon.Zine.Catalog.Tests.Application.Handlers;

public class CreateCategoryHandlerTest
{
    private readonly AutoMocker _mocker;
    private readonly CreateCategoryHandler _handler;

    public CreateCategoryHandlerTest()
    {
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<CreateCategoryHandler>();
    }

    [Fact]
    public async Task ShouldCreateCategory()
    {
        //Arrange
        var command = new CreateCategoryCommand("test", "test description");
        MockCategoryExists(false);

        //Act
        var result = await _handler.Handle(command, default);
        var category = (Category)result.Result;
        
        //Assert
        Assert.Equal("test", category.Name);
        Assert.Equal("test description", category.Description);
    }

    private void MockCategoryExists(bool exists = true)
        => _mocker.GetMock<IUnitOfWork>()
        .Setup(c => c.CategoryRepository.ExistsByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(exists);
}