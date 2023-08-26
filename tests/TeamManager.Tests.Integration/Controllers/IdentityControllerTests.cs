namespace TeamManager.Tests.Integration.Controllers;

using System.Net;
using System.Net.Http.Json;
using Shouldly;
using Api.Users.Requests;
using Application.Users.DTO;
using TeamManager.Core.Shared.ValueObjects;
using Shared;
using Xunit;

public class IdentityControllerTests : ControllerTestBase, IDisposable
{
   private readonly TestDatabase _testDatabase;
   private const string RequestUrl = "identity";

   public IdentityControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
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

      var response = await Client.PostAsJsonAsync($"{RequestUrl}/sign-up", command);
      
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
      
      await CreateModels.CreateUser(_testDatabase, email, password);

      var command = new SignInRequest() { Email = email, Password = password};
      var response = await Client.PostAsJsonAsync($"{RequestUrl}/sign-in", command);
      
      response.StatusCode.ShouldBe(HttpStatusCode.OK);
      var token = await response.Content.ReadFromJsonAsync<AuthResultDto>();
      token.ShouldBeOfType<AuthResultDto>();
      token.AccessToken.ShouldNotBeNull();
      token.RefreshToken.ShouldNotBeNull();
      token.AccessToken.ShouldNotBeNullOrWhiteSpace();
      token.RefreshToken.ShouldNotBeNullOrWhiteSpace();
   }
   
   [Fact]
   public async Task refresh_token_user_should_return_200_status_code_and_auth_token()
   {
      var token = await CreateRefreshToken();

      var command = new RefreshTokenRequest() { RefreshToken = token};
      var response = await Client.PostAsJsonAsync($"{RequestUrl}/token/refresh", command);

      response.StatusCode.ShouldBe(HttpStatusCode.OK);
      var tokenResponse = await response.Content.ReadFromJsonAsync<AuthResultDto>();
      tokenResponse.ShouldBeOfType<AuthResultDto>();
      tokenResponse.AccessToken.ShouldNotBeNull();
      tokenResponse.RefreshToken.ShouldNotBeNull();
      tokenResponse.AccessToken.ShouldNotBeNullOrWhiteSpace();
      tokenResponse.RefreshToken.ShouldNotBeNullOrWhiteSpace();
      tokenResponse.RefreshToken.ShouldBe(token);
   }

   private async Task<string> CreateRefreshToken()
   {
      var user = await CreateModels.CreateUser(_testDatabase);
      var token = RefreshTokenService.Create(new Id(user.Id.Value));
      
      await _testDatabase.DbContext.RefreshToken.AddAsync(token);
      await _testDatabase.DbContext.SaveChangesAsync();
      return token.Token.Value;
   }
}