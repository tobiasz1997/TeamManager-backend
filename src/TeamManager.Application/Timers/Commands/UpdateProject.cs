using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Timers.Commands;

public record UpdateProject(Guid Id, string Label, string Color): ICommand;