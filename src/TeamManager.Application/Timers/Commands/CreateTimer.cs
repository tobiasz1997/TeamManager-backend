using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Timers.Commands;

public record CreateTimer(Guid Id, Guid UserId, Guid? ProjectId, string Description, DateTime Date): ICommand;