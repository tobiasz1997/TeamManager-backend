using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.User.Commands;

public record SignUp(Guid Id, string Email, string Password, string FirstName, string LastName) : ICommand;