using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Assignment.Commands;

public record DeleteAssignment(Guid Id) : ICommand;