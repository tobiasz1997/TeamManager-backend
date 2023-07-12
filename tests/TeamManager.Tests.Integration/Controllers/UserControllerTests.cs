using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using TeamManager.Api.Users.Requests;
using TeamManager.Application.Users.DTO;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Models;
using TeamManager.Infrastructure.Shared.Security;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

public class UserControllerTests : ControllerTestBase, IDisposable
{
   private readonly TestDatabase _testDatabase;

   public UserControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
   {
      _testDatabase = new TestDatabase();
   }
   
   public void Dispose()
   {
      _testDatabase.Dispose();
   }
   
   [Fact]
   public async Task sign_up_user_should_return_200_status_code()
   {
      var command = new SignUpRequest()
         { Email = "grzeg1@vp.pl", Password = "SECRET", FirstName = "Jan", LastName = "Kowalski" };

      var response = await Client.PostAsJsonAsync("user/sign-up", command);
      
      response.StatusCode.ShouldBe(HttpStatusCode.OK);
      var token = await response.Content.ReadFromJsonAsync<AuthResultDto>();
      token.ShouldBeOfType<AuthResultDto>();
      token.AccessToken.ShouldNotBeNull();
      token.RefreshToken.ShouldNotBeNull();
      token.AccessToken.ShouldNotBeNullOrWhiteSpace();
      token.RefreshToken.ShouldNotBeNullOrWhiteSpace();
   }

   [Fact]
   public async Task sign_in_user_should_return_200_status_code_and_auth_result_dto()
   {
      const string email = "grzeg11@wp.pl";
      const string password = "hardPassword123";
      
      await CreateUser(email, password);

      var command = new SignInRequest() { Email = email, Password = password};
      var response = await Client.PostAsJsonAsync("user/sign-in", command);
      
      response.StatusCode.ShouldBe(HttpStatusCode.OK);
      var token = await response.Content.ReadFromJsonAsync<AuthResultDto>();
      token.ShouldBeOfType<AuthResultDto>();
      token.AccessToken.ShouldNotBeNull();
      token.RefreshToken.ShouldNotBeNull();
      token.AccessToken.ShouldNotBeNullOrWhiteSpace();
      token.RefreshToken.ShouldNotBeNullOrWhiteSpace();
   }
   
   [Fact]
   public async Task get_user_me_should_return_200_status_code_and_user_dto()
   {
      const string email = "grzeg11@wp.pl";
      const string password = "hardPassword123";
      
      var user = await CreateUser(email, password);
      Authorize(user.Id, user.Email);

      var userDto = await Client.GetFromJsonAsync<UserDto>("user/me");

      userDto.ShouldNotBeNull();
      userDto.Id.ShouldBe(user.Id.Value);
      userDto.Email.ShouldBe(user.Email.Value);
      userDto.FirstName.ShouldBe(user.FirstName.Value);
      userDto.LastName.ShouldBe(user.LastName.Value);
   }
   
   [Fact]
   public async Task refresh_token_user_should_return_200_status_code_and_auth_token()
   {
      var token = await CreateRefreshToken();

      var command = new RefreshTokenRequest() { RefreshToken = token};
      var response = await Client.PostAsJsonAsync("user/token/refresh", command);

      response.StatusCode.ShouldBe(HttpStatusCode.OK);
      var tokenResponse = await response.Content.ReadFromJsonAsync<AuthResultDto>();
      tokenResponse.ShouldBeOfType<AuthResultDto>();
      tokenResponse.AccessToken.ShouldNotBeNull();
      tokenResponse.RefreshToken.ShouldNotBeNull();
      tokenResponse.AccessToken.ShouldNotBeNullOrWhiteSpace();
      tokenResponse.RefreshToken.ShouldNotBeNullOrWhiteSpace();
      tokenResponse.RefreshToken.ShouldBe(token);
   }

   private async Task<User> CreateUser(string email, string password)
   {
      var passwordService = new PasswordService(new PasswordHasher<User>());
      var user = new User(Guid.NewGuid(), email, passwordService.Secure(password), "Jan", "Kowalski", DateTime.UtcNow);

      await _testDatabase.DbContext.User.AddAsync(user);
      await _testDatabase.DbContext.SaveChangesAsync();

      return user;
   }
   
   private async Task<string> CreateRefreshToken()
   {
      const string email = "grzeg11@wp.pl";
      const string password = "hardPassword123";
      var passwordService = new PasswordService(new PasswordHasher<User>());
      var user = new User(Guid.NewGuid(), email, passwordService.Secure(password), "Jan", "Kowalski", DateTime.UtcNow);
      var token = refreshTokenService.Create(new Id(user.Id.Value));

      await _testDatabase.DbContext.User.AddAsync(user);
      await _testDatabase.DbContext.RefreshToken.AddAsync(token);
      await _testDatabase.DbContext.SaveChangesAsync();
      return token.Token.Value;
   }
}