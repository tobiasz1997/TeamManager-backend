using System.ComponentModel.DataAnnotations;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Api.Assignments.Requests;

public class UpdateAssignmentStatusRequest
{
    [property: Required] 
    public Guid Id { get; set; }
    [property: Required] 
    public AssignmentStatusType Status { get; set; }
}