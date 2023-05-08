namespace TeamManager.Application.Shared.Abstractions.Exceptions;

public abstract class UnauthorizedException : Exception
{
    protected UnauthorizedException(string message) : base(message)
    {
        
    }
}