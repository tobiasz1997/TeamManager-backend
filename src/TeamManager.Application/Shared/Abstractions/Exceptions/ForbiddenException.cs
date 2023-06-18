namespace TeamManager.Application.Shared.Abstractions.Exceptions;

public abstract class ForbiddenException: Exception
{
    protected ForbiddenException(string message) : base(message)
    {
        
    }
}