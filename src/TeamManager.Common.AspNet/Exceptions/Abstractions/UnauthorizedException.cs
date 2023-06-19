namespace TeamManager.Common.AspNet.Exceptions.Abstractions;

public abstract class UnauthorizedException : Exception
{
    protected UnauthorizedException(string message) : base(message)
    {
        
    }
}