using System.Text.RegularExpressions;
using TeamManager.Core.Shared.Exceptions;
using TeamManager.Core.Timer.Exceptions;

namespace TeamManager.Core.Timer.ValueObjects;

public class Color
{
    private static readonly Regex Regex = new(
        @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$",
        RegexOptions.Compiled);
    public string Value { get; }

    public Color(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyValueException(nameof(Color));
        }

        if (!Regex.IsMatch(value))
        {
            throw new InvalidColorException(value);
        }

        Value = value;
    }

    public static implicit operator string(Color color) => color.Value;
    public static implicit operator Color(string color) => new(color);
}