using FirebaseAdmin;
using FirebaseAdmin.Auth;
public class FirebaseTokenValidatorMiddleware
{
    private readonly RequestDelegate _next;

    // Allows the request to pass to the next middleware in the pipeline if the token is valid
    public FirebaseTokenValidatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get the token from the Authorization header
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                // Verify the token
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                Console.WriteLine("Token is valid");
                // Add user information to the context
                context.Items["UserEmail"] = decodedToken.Claims["email"].ToString();
            }
            catch (FirebaseAuthException)
            {
                // Token is invalid
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                Console.WriteLine("Token is invalid");
                return;
            }
        }

        await _next(context);
    }
}
