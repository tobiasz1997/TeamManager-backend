using TeamManager.Application.Users.DTO;

namespace TeamManager.Application.Shared.Services;

public interface IAccessTokenStorage
{
    void Set(AuthResultDto jwt);
    AuthResultDto Get();
}