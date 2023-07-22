using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Users.ValueObjects;

public sealed record FirstName
{

    public string Value { get; }

    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException("First name");
        }

        Value = value;
    }

    public static implicit operator string(FirstName firstName) => firstName.Value;

    public static implicit operator FirstName(string firstName) => new(firstName);
}