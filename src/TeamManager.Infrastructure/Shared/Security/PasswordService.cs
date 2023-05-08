using Microsoft.AspNetCore.Identity;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.User.Models;

namespace TeamManager.Infrastructure.Shared.Security;

internal sealed class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string securePassword) =>
        _passwordHasher.VerifyHashedPassword(default, securePassword, password) is PasswordVerificationResult.Success;
}