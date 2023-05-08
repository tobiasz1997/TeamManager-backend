namespace TeamManager.Application.Shared.Services;

public interface IPasswordService
{
    string Secure(string password);
    bool Validate(string password, string securePassword);
}