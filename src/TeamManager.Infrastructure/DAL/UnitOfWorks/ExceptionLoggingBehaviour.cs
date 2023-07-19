using Humanizer;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Infrastructure.DAL.UnitOfWorks;

public class ExceptionLoggingBehaviour<TMessage, TResponse> : MessageExceptionHandler<TMessage, TResponse>
    where TMessage : notnull, IMessage
{
    private readonly ILogger<ExceptionLoggingBehaviour<TMessage, TResponse>> _logger;

    public ExceptionLoggingBehaviour(ILogger<ExceptionLoggingBehaviour<TMessage, TResponse>> logger)
    {
        _logger = logger;
    }

    protected override ValueTask<ExceptionHandlingResult<TResponse>> Handle(TMessage message, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private static async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            BadRequestException => (StatusCodes.Status400BadRequest, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            ForbiddenException => (StatusCodes.Status403Forbidden, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            NotFoundException => (StatusCodes.Status404NotFound, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            MethodNotAllowedException => (StatusCodes.Status405MethodNotAllowed, new ErrorResponse(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new ErrorResponse("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}