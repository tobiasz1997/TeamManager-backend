using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.User.Commands;

public record RefreshToken(string Token) : ICommand;