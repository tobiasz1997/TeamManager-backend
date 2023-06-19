namespace TeamManager.Common.AspNet.Exceptions.Abstractions;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message) : base(message)
    {
        
    }
}