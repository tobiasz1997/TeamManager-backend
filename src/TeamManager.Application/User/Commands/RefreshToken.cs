using TeamManager.Application.Shared.Abstractions.Commands;

namespace TeamManager.Application.User.Commands;

public record RefreshToken(string Token) : ICommand;