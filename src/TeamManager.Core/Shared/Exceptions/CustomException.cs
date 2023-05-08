namespace TeamManager.Core.Shared.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
        
    }
}