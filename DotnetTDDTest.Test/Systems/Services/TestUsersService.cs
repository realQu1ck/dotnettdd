using DotnetTDDTest.API.Config;
using DotnetTDDTest.Test.Helpers;
using Microsoft.Extensions.Options;
using Moq.Protected;

namespace DotnetTDDTest.Test.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        //Arange
        var expectedResult = UsersFixture.GetTestUsers();
        var mockHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResult);
        var httpClient = new HttpClient(mockHandler.Object);

        var endpoint = "https://example.com";
        var config = Options.Create(new API.Config.UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        await sut.GetAllUsers();

        //Assert
        mockHandler.Protected().Verify("SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        //Arange
        var expectedResult = UsersFixture.GetTestUsers();
        var mockHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResult);
        var httpClient = new HttpClient(mockHandler.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new API.Config.UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Count.Should().Be(expectedResult.Count);
    }

    [Fact]
    public async Task GetAllUsers_WhenHit404_ReturnsEmptyListOfUsers()
    {
        //Arange
        var mockHandler = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(mockHandler.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new API.Config.UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        //Arange
        var expectedResult = UsersFixture.GetTestUsers();

        var endpoint = "https://example.com/users";
        var mockHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResult);
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });

        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        mockHandler
            .Protected()
            .Verify
            ("SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(
                req => req.Method == HttpMethod.Get 
                && req.RequestUri.ToString() == endpoint),
            ItExpr.IsAny<CancellationToken>());
    }
}
