using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Assignment.Exceptions;

public sealed class TooLongValueException : CustomException
{
    public string Name { get;  }
    public int CurrentValue { get; }
    public int MaxValue { get; }

    public TooLongValueException(string name, int currentValue, int  maxValue) : base(
        $"{name} is too long. Value length is equal {currentValue} but max is {maxValue}")
    {
        Name = name;
        CurrentValue = currentValue;
        MaxValue = maxValue;
    }
}