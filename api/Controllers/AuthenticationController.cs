using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        // Check if user information has been added to the HttpContext by the middleware
        if (HttpContext.Items.ContainsKey("UserEmail"))
        {
            // You can access the user's email (or other information) here
            var userEmail = HttpContext.Items["UserEmail"].ToString();
            Console.WriteLine($"User email: {userEmail}");

            // Create a session or equivalent user identification logic
            // You can use userEmail to identify the user in your application

            // Return relevant user information if needed
            return Ok(new { Email = userEmail });
        }

        return Unauthorized("Token validation failed or user information not available.");
    }
}
