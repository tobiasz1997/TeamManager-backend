using TeamManager.Common.AspNet.Exceptions.Abstractions;

namespace TeamManager.Common.AspNet.Exceptions;

public class AuthorizationException : UnauthorizedException
{
    public AuthorizationException() : base("Unauthorized")
    {
    }
}