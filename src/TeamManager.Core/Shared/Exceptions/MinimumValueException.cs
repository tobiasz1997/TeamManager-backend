using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Shared.Exceptions;

public class MinimumValueException : MethodNotAllowedException
{
    public MinimumValueException(string name, int currentValue, int  minimumValue) : base(
        $"{name} is too small. Value is equal {currentValue} but minimum is {minimumValue}")
    {
    }
}