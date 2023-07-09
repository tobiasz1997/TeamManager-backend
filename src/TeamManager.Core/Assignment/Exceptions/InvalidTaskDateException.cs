using TeamManager.Common.Core.Exceptions.Abstractions;
using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Assignment.Exceptions;

public sealed class InvalidTaskDateException : MethodNotAllowedException
{
    private DateTime Date { get; }

    public InvalidTaskDateException(DateTime date) : base($"$Invalid date - {date:d} is past than today")
    {
        Date = date;
    }
}