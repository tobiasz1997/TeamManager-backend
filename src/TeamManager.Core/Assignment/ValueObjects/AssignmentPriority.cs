using TeamManager.Core.Assignment.Exceptions;

namespace TeamManager.Core.Assignment.ValueObjects;

public record AssignmentPriority
{
    public int Value { get; }

    public AssignmentPriority(int value)
    {
        Value = value switch
        {
            < 1 => throw new MinimumValueException("Priority", value, 1),
            > 4 => throw new MaximumValueException("Priority", value, 4),
            _ => value
        };
    }

    public static implicit operator int(AssignmentPriority assignmentPriority) => assignmentPriority.Value;
    public static implicit operator AssignmentPriority(int assignmentPriority) => new(assignmentPriority);
}