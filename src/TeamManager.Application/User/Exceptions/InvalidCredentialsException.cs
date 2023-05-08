using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Application.User.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials")
    {
    }
}