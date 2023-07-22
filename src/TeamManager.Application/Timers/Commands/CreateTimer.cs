using Mediator;

namespace TeamManager.Application.Timers.Commands;

public record CreateTimer(Guid Id, Guid UserId, Guid? ProjectId, string Description, DateTime Date): ICommand;