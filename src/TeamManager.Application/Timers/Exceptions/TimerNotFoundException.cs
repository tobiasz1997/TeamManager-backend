using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Timers.Exceptions;

public class TimerNotFoundException : NotFoundException
{
    public TimerNotFoundException(Guid id) : base($"Project with id = {id} is not exist.")
    {
    }
}