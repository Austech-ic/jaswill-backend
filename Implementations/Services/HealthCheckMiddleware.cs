namespace CMS_appBackend.Implementations.Services
{
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;

    public HealthCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/health")
        {
            // Perform custom health checks here
            // For simplicity, always return a healthy status
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Healthy");
        }
        else
        {
            // Continue with the request pipeline
            await _next(context);
        }
    }
    }
}