using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Application.Timers.Commands;

public record DeleteProject(Guid Id) : ICommand;