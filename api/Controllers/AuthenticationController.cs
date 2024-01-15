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
    // Constructor and dependency injections here
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] string firebaseToken)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
            // Create a session or equivalent user identification logic
            return Ok(); // Or return relevant user information
        }
        catch (FirebaseAuthException ex)
        {
            return Unauthorized(ex.Message); // Token verification failed
        }
    }
}
