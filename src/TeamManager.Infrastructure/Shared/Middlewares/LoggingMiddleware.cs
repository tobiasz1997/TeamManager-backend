using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TeamManager.Infrastructure.Shared.Middlewares;

internal sealed class LoggingMiddleware : IMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        await next(context);
        _logger.LogInformation("Completed operation in {Elapsed:0.0000} ms.", watch.Elapsed.TotalMilliseconds);
        watch.Stop();
    }
}