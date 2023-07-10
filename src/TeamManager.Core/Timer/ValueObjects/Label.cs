using TeamManager.Core.Assignment.Exceptions;
using TeamManager.Core.Shared.Exceptions;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Core.Timer.ValueObjects;

public class Label
{
    public string Value { get; }

    public Label(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Label));
        }

        if (value.Length > 50)
        {
            throw new TooLongValueException(nameof(Label), value.Length, 50);
        }

        Value = value;
    }

    public static implicit operator string(Label label) => label.Value;
    public static implicit operator Label(string label) => new(label);
}