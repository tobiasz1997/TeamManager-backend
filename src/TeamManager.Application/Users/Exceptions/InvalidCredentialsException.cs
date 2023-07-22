using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Users.Exceptions;

public class InvalidCredentialsException : MethodNotAllowedException
{
    public InvalidCredentialsException() : base("Invalid credentials")
    {
    }
}