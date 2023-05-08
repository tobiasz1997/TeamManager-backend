using System.ComponentModel.DataAnnotations;
using TeamManager.Core.User.Enums;

namespace TeamManager.Api.Assignment.Requests;

public class CreateAssignmentRequest
{
    [property: Required] 
    public string Name { get; set; }
    [property: Required] 
    public string Description { get; set; }
    [property: Required] 
    public int Priority { get; set; }
    [property: Required]
    public AssignmentStatusType Status { get; set; }
}