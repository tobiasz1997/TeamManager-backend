using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Assignments.Commands;

public record DeleteAssignment(Guid Id) : ICommand;