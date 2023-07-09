using System.ComponentModel.DataAnnotations;
using TeamManager.Core.User.Enums;

namespace TeamManager.Api.Assignment.Requests;

public class CreateAssignmentRequest
{
    [property: Required] 
    public string Name { get; init; }
    [property: Required] 
    public string Description { get; init; }
    [property: Required] 
    public int Priority { get; set; }
    [property: Required]
    public AssignmentStatusType Status { get; init; }
}