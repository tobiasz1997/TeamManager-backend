using Mediator;

namespace TeamManager.Application.Users.Commands;

public record SignUp(Guid Id, string Email, string Password, string FirstName, string LastName) : ICommand;