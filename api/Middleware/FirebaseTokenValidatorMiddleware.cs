using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Logging;

public class FirebaseTokenValidatorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<FirebaseTokenValidatorMiddleware> _logger;

    // Allows the request to pass to the next middleware in the pipeline if the token is valid
    public FirebaseTokenValidatorMiddleware(RequestDelegate next,  ILogger<FirebaseTokenValidatorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get the token from the Authorization header
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                Console.WriteLine($"Received token: {token}");
                
                // Verify the token
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                Console.WriteLine("Token is valid"); // Log token is valid
                // Add user information to the context
                context.Items["UserEmail"] = decodedToken.Claims["email"].ToString();
            }
            catch (FirebaseAuthException ex)
            {
                // Token is invalid
                Console.WriteLine($"Token is invalid: {ex.Message}"); // Log token is invalid
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
         else
        {
            Console.WriteLine("No token found in the Authorization header");
        }

        await _next(context);
    }
}
