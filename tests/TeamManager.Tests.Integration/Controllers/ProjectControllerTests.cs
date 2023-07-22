using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Shouldly;
using TeamManager.Api.Timers.Requests;
using TeamManager.Application.Timers.DTO;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Tests.Integration.Shared;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

public class ProjectControllerTests : ControllerTestBase, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private const string RequestUrl = "project";
    
    public ProjectControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    [Fact]
    public async Task create_project_should_return_200_status_code_and_id()
    {
        await CreateUserAndAuthorize();
        var command = new CreateProjectRequest()
        {
            Label = "Test", Color = "#000"
        };

        var response = await Client.PostAsJsonAsync(RequestUrl, command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var id = await response.Content.ReadFromJsonAsync<string>();
        id.ShouldBeOfType<string>();
        id.ShouldNotBeNull();
    }

    [Fact]
    public async Task update_project_should_return_200_status_code()
    {
        var projectId = await CreateProjectAndAuthorize();
        var command = new UpdateProjectRequest()
        {
            Id = projectId,
            Label = "Update",
            Color = "#111"
        };
        
        var response = await Client.PutAsJsonAsync(RequestUrl, command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task delete_project_should_return_200_status_code()
    {
        var projectId = await CreateProjectAndAuthorize();

        var response = await Client.DeleteAsync($"{RequestUrl}/{projectId}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task get_projects_list_should_return_200_status_code_and_projects_list()
    {
        var projectId = await CreateProjectAndAuthorize();

        var response = await Client.GetStringAsync($"{RequestUrl}/list");
        var responseDeserialized = JsonConvert.DeserializeObject<IEnumerable<ProjectDto>>(response)?.ToList();
        responseDeserialized.ShouldNotBeNull();
        responseDeserialized.First().Id.ShouldBe(projectId);
    }

    private async Task<Id> CreateUserAndAuthorize()
    {
        var user = await CreateModels.CreateUser(_testDatabase);
        Authorize(user.Id, user.Email);
        return user.Id;
    }

    private async Task<Guid> CreateProjectAndAuthorize()
    {
        var userId = await CreateUserAndAuthorize();
        var project = await CreateModels.CreateProject(_testDatabase, userId);
        return project.Id.Value;
    }
}