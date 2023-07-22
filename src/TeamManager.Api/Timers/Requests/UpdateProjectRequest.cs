using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Timers.Requests;

public class UpdateProjectRequest
{
    [property: Required] 
    public Guid Id { get; init; }
    [property: Required] 
    public string Label { get; init; } = string.Empty;
    [property: Required] 
    public string Color { get; init; } = string.Empty;
}