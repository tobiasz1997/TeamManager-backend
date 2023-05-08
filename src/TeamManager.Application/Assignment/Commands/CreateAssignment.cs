using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Core.User.Enums;

namespace TeamManager.Application.Assignment.Commands;

public record CreateAssignment(Guid Id, Guid UserId, string Name, string Description, int Priority, AssignmentStatusType Status) : ICommand;