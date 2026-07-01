using Microsoft.AspNetCore.Http;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEY = "x-api-key";
    private const string VALID_KEY = "GACHSL-SECRET-123";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip swagger
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        // Check header
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        // Validate key
        if (!VALID_KEY.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}