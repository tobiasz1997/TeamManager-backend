using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Users.Commands;

public record SignIn(string Email, string Password) : ICommand;