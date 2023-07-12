using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Timers.Exceptions;

public class InvalidColorException : MethodNotAllowedException
{
    public string Color { get; }

    public InvalidColorException(string color) : base($"Color:'{color}' is invalid.")
    {
        Color = color;
    }
}