using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Assignment.Exceptions;

public sealed class InvalidTaskDateException : CustomException
{
    private DateTime Date { get; }

    public InvalidTaskDateException(DateTime date) : base($"$Invalid date - {date:d} is past than today")
    {
        Date = date;
    }
}