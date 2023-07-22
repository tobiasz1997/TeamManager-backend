using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Timers.Exceptions;

public class ProjectNotFoundException : NotFoundException
{
    public ProjectNotFoundException(Guid id) : base($"Project with id = {id} is not exist.")
    {
    }
}