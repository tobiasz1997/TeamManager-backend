using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Users.Commands;

public record RefreshToken(string Token) : ICommand;