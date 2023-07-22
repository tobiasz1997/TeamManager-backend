using Mediator;

namespace TeamManager.Application.Timers.Commands;

public record UpdateProject(Guid Id, string Label, string Color): ICommand;