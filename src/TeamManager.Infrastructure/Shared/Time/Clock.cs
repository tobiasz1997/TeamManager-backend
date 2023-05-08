using TeamManager.Application.Shared.Services;

namespace TeamManager.Infrastructure.Shared.Time;

public class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}