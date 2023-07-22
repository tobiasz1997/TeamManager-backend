using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Shared.Exceptions;

public sealed class TooLongValueException : MethodNotAllowedException
{
    public TooLongValueException(string name, int currentValue, int  maxValue) : base(
        $"{name} is too long. Value length is equal {currentValue} but max is {maxValue}")
    {
    }
}