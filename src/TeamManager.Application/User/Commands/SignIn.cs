using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.User.Commands;

public record SignIn(string Email, string Password) : ICommand;