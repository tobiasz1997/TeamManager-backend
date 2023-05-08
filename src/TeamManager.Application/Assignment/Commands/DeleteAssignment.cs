using TeamManager.Application.Shared.Abstractions.Commands;

namespace TeamManager.Application.Assignment.Commands;

public record DeleteAssignment(Guid Id) : ICommand;