using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.ValueObjects;

namespace TeamManager.Core.Timers.Models;

public class Timer
{
    public Id Id { get; private set; }
    public Id UserId { get; private set; }
    public Guid? ProjectId { get; private set; }
    public Description Description { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Timer(Id id, Id userId, Guid? projectId, Description description, DateTime date, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        ProjectId = projectId;
        Description = description;
        Date = date;
        CreatedAt = createdAt;
    }

    public void Update(Guid? projectId, Description description, DateTime date)
    {
        ProjectId = projectId;
        Description = description;
        Date = date;
    }
}