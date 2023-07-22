using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Users.Exceptions;

public class AuthenticationException: UnauthorizedException
{
    public AuthenticationException() : base("Unauthenticated")
    {
    }
}