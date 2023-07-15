using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Timers.Commands;

public record DeleteTimer(Guid Id): ICommand;