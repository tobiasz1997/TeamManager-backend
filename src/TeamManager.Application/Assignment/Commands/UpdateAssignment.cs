using TeamManager.Application.Shared.Abstractions.Commands;

namespace TeamManager.Application.Assignment.Commands;

public record UpdateAssignment(Guid Id, string Name, string Description, int Priority) : ICommand;