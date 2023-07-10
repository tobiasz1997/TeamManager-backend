using System.Drawing;
using System.Reflection.Emit;
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Timer.Models;

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