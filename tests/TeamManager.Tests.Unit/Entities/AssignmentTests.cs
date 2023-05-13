using Shouldly;
using TeamManager.Core.Assignment.Exceptions;
using TeamManager.Core.Assignment.Models;
using TeamManager.Core.User.Enums;
using Xunit;

namespace TeamManager.Tests.Unit.Entities;

public class AssignmentTests
{
    #region Arrange
    private readonly Assignment _assignment;

    public AssignmentTests()
    {
        _assignment = new Assignment(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Clean car",
            "Fast",
            2,
            AssignmentStatusType.Done,
            new DateTime());
    }
    #endregion

    [Fact]
    public void given_invalid_priority_update_assignment_should_fail()
    {
        // ARRANGE
        //ACT
        var exception = Record.Exception(() => _assignment.UpdateAssignment("Learn c#", "Use some course", 5));

        //ASSET
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<MaximumValueException>();

    }

    [Fact]
    public void given_valid_data_update_assignment_should_success()
    {
        // ARRANGE
        var name = "Learn english";
        var description = "Description";
        var priority = 2;
        //ACT
        _assignment.UpdateAssignment(name, description, priority);

        //ASSET
        _assignment.ShouldNotBeNull();
        _assignment.Name.Value.ShouldBeSameAs(name);
    }
    
}