using TeamManager.Application.Shared.Abstractions.Exceptions;

namespace TeamManager.Application.User.Exceptions;

public class AuthenticationException: UnauthorizedException
{
    public AuthenticationException() : base("Unauthenticated")
    {
    }
}