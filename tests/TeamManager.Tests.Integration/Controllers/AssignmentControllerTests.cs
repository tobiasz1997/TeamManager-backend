using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Shouldly;
using TeamManager.Api.Assignments.Requests;
using TeamManager.Application.Assignments.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Enums;
using TeamManager.Tests.Integration.Shared;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

public class AssignmentControllerTests : ControllerTestBase, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private const string RequestUrl = "assignment";
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
        
        var response = await Client.PostAsJsonAsync(RequestUrl, command);
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
        
        var assignmentId = await CreateAssignmentAndAuthorize();
        var command = new UpdateAssignmentRequest()
        {
           Id = assignmentId, Name = name, Description = description, Priority = priority, Status = AssignmentStatusType.ToDo
        };
        
        var response = await Client.PutAsJsonAsync(RequestUrl, command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task update_assignment_status_should_return_200_status_code()
    {
        var assignmentId = await CreateAssignmentAndAuthorize();
        var command = new UpdateAssignmentStatusRequest()
        {
            Id = assignmentId, Status = AssignmentStatusType.Done
        };
        
        var response = await Client.PatchAsJsonAsync(RequestUrl, command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task delete_assignment_should_return_200_status_code()
    {
        var assignmentId = await CreateAssignmentAndAuthorize();

        var response = await Client.DeleteAsync($"{RequestUrl}/{assignmentId}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task get_assignment_by_id_should_return_200_status_code_and_assignment()
    {
        var assignmentId = await CreateAssignmentAndAuthorize();

        var assignment = await Client.GetStringAsync($"{RequestUrl}/{assignmentId}");
        var assignmentDeserialized = JsonConvert.DeserializeObject<AssignmentDto>(assignment);
        assignmentDeserialized.ShouldNotBeNull();
        assignmentDeserialized.Id.ShouldBe(assignmentId);
    }
    
    [Fact]
    public async Task get_assignments_list_by__status_type_should_return_200_status_code_and_assignment_list()
    {
        var status = AssignmentStatusType.Done;
        var assignmentId = await CreateAssignmentAndAuthorize(status);

        var assignment = await Client.GetStringAsync($"{RequestUrl}/list?type={status}");
        var assignmentListDeserialized = JsonConvert.DeserializeObject<PagedResult<AssignmentDto>>(assignment);
        assignmentListDeserialized.ShouldNotBeNull();
        assignmentListDeserialized.TotalResults.ShouldBe(1);
        assignmentListDeserialized.Items.First().Id.ShouldBe(assignmentId);
    }
    
    [Fact]
    public async Task get_assignments_lists_should_return_200_status_code_and_assignment_lists()
    {
        var status = AssignmentStatusType.Aborted;
        var assignmentId = await CreateAssignmentAndAuthorize(status);

        var assignment = await Client.GetStringAsync($"{RequestUrl}/lists");
        var assignmentListDeserialized = JsonConvert.DeserializeObject<AssignmentsListsDto>(assignment);
        assignmentListDeserialized.ShouldNotBeNull();
        assignmentListDeserialized.Done.TotalResults.ShouldBe(0);
        assignmentListDeserialized.Aborted.TotalResults.ShouldBe(1);
        assignmentListDeserialized.Aborted.Items.First().Id.ShouldBe(assignmentId);
    }
    
    private async Task<Id> CreateUserAndAuthorize()
    {
        var user = await CreateModels.CreateUser(_testDatabase);
        Authorize(user);
        return user.Id;
    }

    private async Task<Guid> CreateAssignmentAndAuthorize(AssignmentStatusType status = AssignmentStatusType.ToDo)
    {
        var userId = await CreateUserAndAuthorize();
        var assignment = await CreateModels.CreateAssignment(_testDatabase, userId, status);
        return assignment.Id.Value;
    }
    
}