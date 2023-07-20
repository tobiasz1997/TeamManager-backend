using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.Shared.ValueObjects;

public sealed record Id
{
    public Guid Value { get; }

    public Id(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyValueException(nameof(Id));
        }

        Value = value;
    }

    public static implicit operator Guid(Id id) => id.Value;

    public static implicit operator Id(Guid id) => new(id);
}