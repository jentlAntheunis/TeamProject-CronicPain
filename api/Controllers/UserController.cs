using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;


using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;


[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public UserController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        Console.WriteLine("GetUsersAsync called");
        var users = await _userService.GetUsersAsync();
        if (users == null)
        {
            Console.WriteLine("GetUsersAsync returned null");
            return StatusCode(500);
        }
        Console.WriteLine($"GetUsersAsync returned {users.Count()} users");
        return Ok(JsonConvert.SerializeObject(users));
    }

    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(user));
    }

    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] User user)
    {
        var newUser = await _userService.AddUserAsync(user);
        return Ok(JsonConvert.SerializeObject(newUser));
    }

    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] User user)
    {
        user.Id = id;
        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(JsonConvert.SerializeObject(updatedUser));
    }

    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }

    [HttpGet("CheckIfUserExists/{email}")]
    public async Task<IActionResult> CheckIfUserExistsAsync(string email)
    {
        var userExists = await _userService.CheckIfUserExistsAsync(email);
        return Ok(JsonConvert.SerializeObject(userExists));
    }
}