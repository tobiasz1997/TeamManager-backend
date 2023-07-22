using Mediator;

namespace TeamManager.Application.Timers.Commands;

public record DeleteProject(Guid Id) : ICommand;