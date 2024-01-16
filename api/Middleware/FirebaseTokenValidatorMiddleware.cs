using FirebaseAdmin;
using FirebaseAdmin.Auth;
public class FirebaseTokenValidatorMiddleware
{
    private readonly RequestDelegate _next;

    public FirebaseTokenValidatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                // Add user information to the context or perform additional checks
            }
            catch (FirebaseAuthException)
            {
                // Token is invalid - handle accordingly
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await _next(context);
    }
}
