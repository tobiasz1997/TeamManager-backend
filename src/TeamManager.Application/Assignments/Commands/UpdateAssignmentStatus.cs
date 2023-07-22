using Mediator;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Application.Assignments.Commands;

public record UpdateAssignmentStatus(Guid Id, AssignmentStatusType Status) : ICommand;