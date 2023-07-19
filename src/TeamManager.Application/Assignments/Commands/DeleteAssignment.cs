using Mediator;

namespace TeamManager.Application.Assignments.Commands;

public record DeleteAssignment(Guid Id) : ICommand;