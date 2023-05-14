using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Shouldly;
using TeamManager.Api.Assignment.Requests;
using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Shared.Abstractions.Browsing;
using TeamManager.Core.Assignment.Models;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Enums;
using TeamManager.Core.User.Models;
using TeamManager.Infrastructure.Shared.Security;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

public class AssignmentControllerTests : ControllerTestBase, IDisposable
{
    private readonly TestDatabase _testDatabase;
    public AssignmentControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    [Fact]
    public async Task create_assignment_should_return_200_status_code_and_id()
    {
        await CreateUserAndAuthorize();
        var command = new CreateAssignmentRequest()
        {
            Name = "Clean room", Description = "Short description", Priority = 1, Status = AssignmentStatusType.ToDo
        };
        
        var response = await Client.PostAsJsonAsync("assignment", command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var id = await response.Content.ReadFromJsonAsync<string>();
        id.ShouldBeOfType<string>();
        id.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task update_assignment_should_return_200_status_code()
    {
        const string name = "Do homework";
        const string description = "Must be done at the end of day";
        const int priority = 3;
        
        var assignmentId = await CreateAssignment();
        var command = new UpdateAssignmentRequest()
        {
           Id = assignmentId, Name = name, Description = description, Priority = priority, Status = AssignmentStatusType.ToDo
        };
        
        var response = await Client.PutAsJsonAsync("assignment", command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task update_assignment_status_should_return_200_status_code()
    {
        var assignmentId = await CreateAssignment();
        var command = new UpdateAssignmentStatusRequest()
        {
            Id = assignmentId, Status = AssignmentStatusType.Done
        };
        
        var response = await Client.PatchAsJsonAsync("assignment", command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task delete_assignment_should_return_200_status_code()
    {
        var assignmentId = await CreateAssignment();

        var response = await Client.DeleteAsync($"assignment/{assignmentId}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task get_assignment_by_id_should_return_200_status_code_and_assignment()
    {
        var assignmentId = await CreateAssignment();

        var assignment = await Client.GetStringAsync($"assignment/{assignmentId}");
        var assignmentDeserialized = JsonConvert.DeserializeObject<AssignmentDto>(assignment);
        assignmentDeserialized.ShouldNotBeNull();
        assignmentDeserialized.Id.ShouldBe(assignmentId);
    }
    
    [Fact]
    public async Task get_assignment_List_should_return_200_status_code_and_assignment_list()
    {
        var status = AssignmentStatusType.Done;
        var assignmentId = await CreateAssignment(status);

        var assignment = await Client.GetStringAsync($"assignment/list?type={status}");
        var assignmentListDeserialized = JsonConvert.DeserializeObject<PagedResult<AssignmentDto>>(assignment);
        assignmentListDeserialized.ShouldNotBeNull();
        assignmentListDeserialized.TotalResults.ShouldBe(1);
        assignmentListDeserialized.Items.First().Id.ShouldBe(assignmentId);
    }
    
    [Fact]
    public async Task get_assignment_Lists_should_return_200_status_code_and_assignment_lists()
    {
        var status = AssignmentStatusType.Aborted;
        var assignmentId = await CreateAssignment(status);

        var assignment = await Client.GetStringAsync($"assignment/lists");
        var assignmentListDeserialized = JsonConvert.DeserializeObject<AssignmentsListsDto>(assignment);
        assignmentListDeserialized.ShouldNotBeNull();
        assignmentListDeserialized.Done.TotalResults.ShouldBe(0);
        assignmentListDeserialized.Aborted.TotalResults.ShouldBe(1);
        assignmentListDeserialized.Aborted.Items.First().Id.ShouldBe(assignmentId);
    }
    
    private async Task<Id> CreateUserAndAuthorize()
    {
        const string email = "grzeg11@wp.pl";
        const string password = "hardPassword123";
        
        var passwordService = new PasswordService(new PasswordHasher<User>());
        var user = new User(Guid.NewGuid(), email, passwordService.Secure(password), "Jan", "Kowalski", DateTime.UtcNow);

        await _testDatabase.DbContext.User.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();

        Authorize(user.Id, user.Email);

        return user.Id;
    }

    private async Task<Guid> CreateAssignment(AssignmentStatusType status = AssignmentStatusType.ToDo)
    {
        var userId = await CreateUserAndAuthorize();
        var assignmentId = Guid.NewGuid();
        var assignment = new Assignment(assignmentId, userId, "Clean bathroom", "Fast", 1, status,
            DateTime.UtcNow);
        
        await _testDatabase.DbContext.Assignments.AddAsync(assignment);
        await _testDatabase.DbContext.SaveChangesAsync();

        return assignment.Id.Value;
    }
    
}