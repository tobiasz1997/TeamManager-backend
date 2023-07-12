using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Users.ValueObjects;

public sealed record Token
{

    public string Value { get; }

    public Token(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException("Token");
        }

        Value = value;
    }

    public static implicit operator string(Token token) => token.Value;

    public static implicit operator Token(string token) => new(token);
}