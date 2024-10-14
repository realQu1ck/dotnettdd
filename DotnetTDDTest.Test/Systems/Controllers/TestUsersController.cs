namespace DotnetTDDTest.Test.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arange
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = (OkObjectResult)await sut.GetUsers();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUsersService()
    {
        //Arange
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = (OkObjectResult)await sut.GetUsers();

        //Assert
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        //Arange
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = (OkObjectResult)await sut.GetUsers();

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        result.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNotFounded_ReturnsListOfUsers()
    {
        //Arange
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = await sut.GetUsers();

        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}
