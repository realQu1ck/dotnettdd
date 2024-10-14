using DotnetTDDTest.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTDDTest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsers();
        if (users.Any())
            return Ok(users);

        return NotFound();
    }
}
