using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Timers.Commands;

public record CreateProject(Guid Id, Guid UserId, string Label, string Color): ICommand;