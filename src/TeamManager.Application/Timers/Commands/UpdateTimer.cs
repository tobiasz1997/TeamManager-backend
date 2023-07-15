using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Timers.Commands;

public record UpdateTimer(Guid Id, Guid? ProjectId, string Description, DateTime Date): ICommand;