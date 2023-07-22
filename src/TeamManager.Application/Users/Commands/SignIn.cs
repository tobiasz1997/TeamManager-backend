using Mediator;

namespace TeamManager.Application.Users.Commands;

public record SignIn(string Email, string Password) : ICommand;