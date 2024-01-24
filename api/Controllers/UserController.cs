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

namespace Pebbles.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(
            IConfiguration configuration,
            IUserService userService
            )
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
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] User user)
        {
            user.Id = userId;
            var updatedUser = await _userService.UpdateUserAsync(user);
            return Ok(JsonConvert.SerializeObject(updatedUser));
        }

        [HttpGet("exists/{email}")]
        public async Task<IActionResult> CheckIfUserExistsAsync(string email)
        {
            var userExists = await _userService.CheckIfUserExistsAsync(email);
            return Ok(JsonConvert.SerializeObject(userExists));
        }

        [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(user);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
        [HttpPost("loginbyemail")]
        public async Task<IActionResult> LoginAsync([FromBody] string email)
        {
            var user = await _userService.LoginAsync(email);
            if (user == null)
                return NotFound();
            return Ok(JsonConvert.SerializeObject(user));
        }
    }
}

namespace Pebbles.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(
            IConfiguration configuration,
            IUserService userService
            )
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
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] User user)
        {
            user.Id = userId;
            var updatedUser = await _userService.UpdateUserAsync(user);
            return Ok(JsonConvert.SerializeObject(updatedUser));
        }

        [HttpGet("exists/{email}")]
        public async Task<IActionResult> CheckIfUserExistsAsync(string email)
        {
            var userExists = await _userService.CheckIfUserExistsAsync(email);
            return Ok(JsonConvert.SerializeObject(userExists));
        }

        [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(user);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "FirebaseAuthentication")] //only authenticated users can access this controller
        [HttpPost("loginbyemail")]
        public async Task<IActionResult> LoginAsync([FromBody] string email)
        {
            var user = await _userService.LoginAsync(email);
            if (user == null)
                return NotFound();
            return Ok(JsonConvert.SerializeObject(user));
        }
    }
}