namespace TeamManager.Core.Shared.Exceptions;

public sealed class EmptyValueException : CustomException
{
    public string Name { get;  }
    public EmptyValueException(string name) : base($"{name} is empty.")
    {
        Name = name;
    }
}