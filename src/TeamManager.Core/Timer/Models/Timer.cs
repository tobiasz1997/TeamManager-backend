using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timer.ValueObjects;

namespace TeamManager.Core.Timer.Models;

public class Timer
{
    public Id Id { get; private set; }
    public Id UserId { get; private set; }
    public Guid? Project { get; private set; }
    public Description Description { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Timer(Id id, Id userId, Guid? project, Description description, DateTime date, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Project = project;
        Description = description;
        Date = date;
        CreatedAt = createdAt;
    }

    public void Update(Guid? project, Description description, DateTime date)
    {
        Project = project;
        Description = description;
        Date = date;
    }
}