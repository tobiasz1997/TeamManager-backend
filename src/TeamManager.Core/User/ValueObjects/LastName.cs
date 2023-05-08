using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.User.ValueObjects;

public sealed record LastName
{

    public string Value { get; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException("Last name");
        }

        Value = value;
    }

    public static implicit operator string(LastName lastName) => lastName.Value;

    public static implicit operator LastName(string lastName) => new(lastName);
}