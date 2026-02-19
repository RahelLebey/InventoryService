namespace Maintenance.WebAPI.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Allow Swagger and root path without API key
        if (context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path == "/")
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = "API key is missing."
            });
            return;
        }

        var apiKey = _configuration["ApiSettings:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey) || !string.Equals(apiKey, extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = "Invalid API key."
            });
            return;
        }

        await _next(context);
    }
}
