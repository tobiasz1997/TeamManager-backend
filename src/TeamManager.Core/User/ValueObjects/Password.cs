using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.User.ValueObjects;

public sealed record Password
{
    public string Value { get; }
        
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 200 or < 6)
        {
            throw new EmptyValueException("Password");
        }

        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value.Value;
}