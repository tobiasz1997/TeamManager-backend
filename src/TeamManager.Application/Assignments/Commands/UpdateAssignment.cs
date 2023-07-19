using Mediator;

namespace TeamManager.Application.Assignments.Commands;

public record UpdateAssignment(Guid Id, string Name, string Description, int Priority) : ICommand;