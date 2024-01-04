using System.Net.Http.Json;
using Shouldly;
using TeamManager.Application.Users.DTO;
using TeamManager.Tests.Integration.Shared;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

public class UserControllerTests : ControllerTestBase, IDisposable
{
   private readonly TestDatabase _testDatabase;
   private const string RequestUrl = "user";

   public UserControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
   {
      _testDatabase = new TestDatabase();
   }
   
   public void Dispose()
   {
      _testDatabase.Dispose();
   }
   
   [Fact]
   public async Task get_user_me_should_return_200_status_code_and_user_dto()
   {
      const string email = "grzeg11@wp.pl";
      const string password = "hardPassword123";
      
      var user = await CreateModels.CreateUser(_testDatabase, email, password);
      Authorize(user);

      var userDto = await Client.GetFromJsonAsync<UserDto>($"{RequestUrl}/me");

      userDto.ShouldNotBeNull();
      userDto.Id.ShouldBe(user.Id.Value);
      userDto.Email.ShouldBe(user.Email.Value);
      userDto.FirstName.ShouldBe(user.FirstName.Value);
      userDto.LastName.ShouldBe(user.LastName.Value);
   }
}