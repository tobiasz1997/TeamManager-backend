using Mediator;

namespace TeamManager.Application.Users.Commands;

public record RefreshToken(string Token) : ICommand;