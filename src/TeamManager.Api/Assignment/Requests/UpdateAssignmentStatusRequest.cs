using System.ComponentModel.DataAnnotations;
using TeamManager.Core.User.Enums;

namespace TeamManager.Api.Assignment.Requests;

public class UpdateAssignmentStatusRequest
{
    [property: Required] 
    public Guid Id { get; set; }
    [property: Required] 
    public AssignmentStatusType Status { get; set; }
}