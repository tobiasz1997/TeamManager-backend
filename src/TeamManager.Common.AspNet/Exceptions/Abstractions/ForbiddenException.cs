namespace TeamManager.Common.AspNet.Exceptions.Abstractions;

public abstract class ForbiddenException: Exception
{
    protected ForbiddenException(string message) : base(message)
    {
        
    }
}