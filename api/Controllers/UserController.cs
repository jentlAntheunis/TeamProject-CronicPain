using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
        _userService = new UserService(_configuration);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await _userService.GetUsersAsync();
        if(users == null)
        {
            return StatusCode(500);
        }
        return Ok(JsonConvert.SerializeObject(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(user));
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] User user)
    {
        var newUser = await _userService.AddUserAsync(user);
        return Ok(JsonConvert.SerializeObject(newUser));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] User user)
    {
        user.Id = id;
        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(JsonConvert.SerializeObject(updatedUser));
    }

    [HttpGet("CheckIfUserExists/{email}")]
    public async Task<IActionResult> CheckIfUserExistsAsync(string email)
    {
        var userExists = await _userService.CheckIfUserExistsAsync(email);
        return Ok(JsonConvert.SerializeObject(userExists));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if(user == null)
        {
            return NotFound();
        }
        await _userService.DeleteUserAsync(user);
        return Ok();
    }
}