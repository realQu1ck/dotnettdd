using DotnetTDDTest.API.Config;
using DotnetTDDTest.API.Models;

namespace DotnetTDDTest.API.Services;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
}

public class UserService : IUserService
{
    private readonly HttpClient httpClient;
    private readonly UsersApiOptions apiConfig;

    public UserService(HttpClient httpClient, 
        Microsoft.Extensions.Options.IOptions<UsersApiOptions> options)
    {
        this.httpClient = httpClient;
        apiConfig = options.Value;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var usersResponse = await httpClient
            .GetAsync(apiConfig.Endpoint);

        if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            return new List<User>();

        var responseContent = usersResponse.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();
    }
}
