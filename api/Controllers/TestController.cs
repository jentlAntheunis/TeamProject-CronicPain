using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;

[ApiController]
[Route("tests")]
public class TestController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public TestController(IConfiguration configuration)
    {
        _configuration = configuration;
        _userService = new UserService(_configuration);
    }
}