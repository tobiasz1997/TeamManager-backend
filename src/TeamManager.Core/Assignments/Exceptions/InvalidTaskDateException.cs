using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Assignments.Exceptions;

public sealed class InvalidTaskDateException : MethodNotAllowedException
{
    private DateTime Date { get; }

    public InvalidTaskDateException(DateTime date) : base($"$Invalid date - {date:d} is past than today")
    {
        Date = date;
    }
}