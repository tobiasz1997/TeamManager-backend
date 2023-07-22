using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Timers.ValueObjects;

public class Description
{
    public string Value { get; }
    
    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Description));
        }

        if (value.Length > 50)
        {
            throw new TooLongValueException(nameof(Description), value.Length, 50);
        }

        Value = value;
    }
    
    public static implicit operator string(Description description) => description.Value;
    public static implicit operator Description(string description) => new(description);
}