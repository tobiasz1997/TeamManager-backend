using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Shouldly;
using TeamManager.Api.Timers.Requests;
using TeamManager.Application.Timers.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Tests.Integration.Shared;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

public class TimerControllerTests : ControllerTestBase, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private const string RequestUrl = "timer";
    
    public TimerControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
    
    [Fact]
    public async Task create_timer_should_return_200_status_code_and_id()
    {
        await CreateUserAndAuthorize();
        var command = new CreateTimerRequest()
        {
            ProjectId = null,
            Description = "Test", 
            Date = DateTime.UtcNow
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
        var timerId = await CreateTimerAndAuthorize();
        var command = new UpdateTimerRequest()
        {
            Id = timerId,
            ProjectId = null,
            Description = "Update",
            Date = DateTime.UtcNow
        };
        
        var response = await Client.PutAsJsonAsync(RequestUrl, command);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task delete_project_should_return_200_status_code()
    {
        var timerId = await CreateTimerAndAuthorize();

        var response = await Client.DeleteAsync($"{RequestUrl}/{timerId}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task get_projects_list_should_return_200_status_code_and_projects_list()
    {
        var timerId = await CreateTimerAndAuthorize();

        var response = await Client.GetStringAsync($"{RequestUrl}/list");
        var responseDeserialized = JsonConvert.DeserializeObject<PagedResult<TimerDto>>(response);
        responseDeserialized.ShouldNotBeNull();
        responseDeserialized.TotalResults.ShouldBe(1);
        responseDeserialized.Items.First().Id.ShouldBe(timerId);
    }

    private async Task<Id> CreateUserAndAuthorize()
    {
        var user = await CreateModels.CreateUser(_testDatabase);
        Authorize(user);
        return user.Id;
    }

    private async Task<Guid> CreateTimerAndAuthorize()
    {
        var userId = await CreateUserAndAuthorize();
        var timer = await CreateModels.CreateTimer(_testDatabase, userId);
        return timer.Id.Value;
    }
    
}