using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.User.Exceptions;

public class AuthenticationException: UnauthorizedException
{
    public AuthenticationException() : base("Unauthenticated")
    {
    }
}