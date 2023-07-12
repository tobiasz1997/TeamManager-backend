using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Shared.Exceptions;

public class MaximumValueException : MethodNotAllowedException
{
    public string Name { get;  }
    public int CurrentValue { get; }
    public int MaximumValue { get; }

    public MaximumValueException(string name, int currentValue, int  maximumValue) : base(
        $"{name} is too big. Value is equal {currentValue} but minimum is {maximumValue}")
    {
        Name = name;
        CurrentValue = currentValue;
        MaximumValue = maximumValue;
    }
}