using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.ValueObjects;

namespace TeamManager.Core.Timers.Models;

public class Project
{
    public Id Id { get; private set; }
    public Id UserId { get; private set; }
    public Label Label { get; private set;  }
    public Color Color { get; private set;  }
    public DateTime CreatedAt { get; private set; }

    public Project(Id id,Id userId, Label label, Color color, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Label = label;
        Color = color;
        CreatedAt = createdAt;
    }
}