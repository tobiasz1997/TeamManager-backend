namespace TeamManager.Common.Core.Exceptions.Abstractions;

public abstract class MethodNotAllowedException : Exception
{
    protected MethodNotAllowedException(string message) : base(message)
    {
        
    }
}