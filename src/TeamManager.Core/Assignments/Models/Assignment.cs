using TeamManager.Core.Assignments.ValueObjects;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Core.Assignments.Models;

public class Assignment
{
    public Id Id { get; private set; }
    public Id UserId { get; private set; }
    public AssignmentName Name { get; private set; }
    public AssignmentDescription Description { get; private set; }
    public AssignmentPriority Priority { get; private set; }
    public AssignmentStatusType Status { get; private set; }
    public DateTime StartDate { get; private set; }

    public Assignment(Id id, Id userId, AssignmentName name, AssignmentDescription description, AssignmentPriority priority, AssignmentStatusType status, DateTime startDate)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Description = description;
        Priority = priority;
        Status = status;
        StartDate = startDate;
    }

    public void UpdateAssignmentStatus(AssignmentStatusType status)
    {
        Status = status;
    }
    
    public void UpdateAssignment(AssignmentName name, AssignmentDescription description, AssignmentPriority priority)
    {
        Name = name;
        Description = description;
        Priority = priority;
    }
}