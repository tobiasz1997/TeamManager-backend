using TeamManager.Core.Assignment.Exceptions;
using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Assignment.ValueObjects;

public record AssignmentName
{
    public string Value { get; }

    public AssignmentName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException("Name");
        }

        if (value.Length > 50)
        {
            throw new TooLongValueException("Name", value.Length, 50);
        }

        Value = value;
    }

    public static implicit operator string(AssignmentName assignmentName) => assignmentName.Value;
    public static implicit operator AssignmentName(string assignmentName) => new(assignmentName);
}