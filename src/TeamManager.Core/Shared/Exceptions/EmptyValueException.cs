using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Shared.Exceptions;

public sealed class EmptyValueException : MethodNotAllowedException
{
    public string Name { get;  }
    public EmptyValueException(string name) : base($"{name} is empty.")
    {
        Name = name;
    }
}