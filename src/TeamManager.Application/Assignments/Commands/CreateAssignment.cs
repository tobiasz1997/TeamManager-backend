using Mediator;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Application.Assignments.Commands;

public record CreateAssignment(Guid Id, Guid UserId, string Name, string Description, int Priority, AssignmentStatusType Status) : ICommand;