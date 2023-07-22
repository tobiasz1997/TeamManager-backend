using Mediator;

namespace TeamManager.Application.Timers.Commands;

public record DeleteTimer(Guid Id): ICommand;