namespace TeamManager.Application.Shared.Services;

public interface IJwtService
{
    string CreateToken(Guid userId, string email);
}