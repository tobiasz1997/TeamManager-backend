using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.Shared.Abstractions.Browsing;

public record PagedResult<TItem>
{
    [property: Required]
    public long TotalResults { get; }
    [property: Required]
    public List<TItem> Items { get; }

    
    public PagedResult(IEnumerable<TItem> items, long totalResults)
    {
        Items = items.ToList();
        TotalResults = totalResults;
    }
}