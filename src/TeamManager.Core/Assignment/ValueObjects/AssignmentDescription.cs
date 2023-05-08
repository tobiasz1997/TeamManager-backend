using TeamManager.Core.Assignment.Exceptions;

namespace TeamManager.Core.Assignment.ValueObjects;

public record AssignmentDescription
{
    public string Value { get; }

    public AssignmentDescription(string value)
    {
        if (value.Length > 50)
        {
            throw new TooLongValueException("Description", value.Length, 50);
        }

        Value = value;
    }
    
    public static implicit operator string(AssignmentDescription assignmentDescription) => assignmentDescription.Value;
    public static implicit operator AssignmentDescription(string assignmentDescription) => new(assignmentDescription);
}