using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.User.Exceptions;

public class InvalidCredentialsException : MethodNotAllowedException
{
    public InvalidCredentialsException() : base("Invalid credentials")
    {
    }
}