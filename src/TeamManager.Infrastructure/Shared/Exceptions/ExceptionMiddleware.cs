using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TeamManager.Application.Shared.Abstractions.Exceptions;
using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Infrastructure.Shared.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private static async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            BadRequestException => (StatusCodes.Status400BadRequest, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            ForbiddenException => (StatusCodes.Status403Forbidden, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            NotFoundException => (StatusCodes.Status404NotFound, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new ErrorResponse("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}