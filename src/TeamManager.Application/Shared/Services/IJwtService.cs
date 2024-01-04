using TeamManager.Core.Users.Models;

namespace TeamManager.Application.Shared.Services;

public interface IJwtService
{
    string CreateToken(User user);
}