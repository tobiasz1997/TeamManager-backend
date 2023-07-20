using System.ComponentModel.DataAnnotations;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Api.Assignments.Requests;

public class CreateAssignmentRequest
{
    [property: Required] 
    public string Name { get; init; } = string.Empty;
    [property: Required] 
    public string Description { get; init; } = string.Empty;
    [property: Required] 
    public int Priority { get; set; }
    [property: Required]
    public AssignmentStatusType Status { get; init; }
}