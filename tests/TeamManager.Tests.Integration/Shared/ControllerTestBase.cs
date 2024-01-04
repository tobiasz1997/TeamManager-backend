using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.Users.Models;
using TeamManager.Infrastructure.Shared.Auth;
using TeamManager.Infrastructure.Shared.Time;
using Xunit;

namespace TeamManager.Tests.Integration.Shared;

[Collection("api")]
public abstract class ControllerTestBase : IClassFixture<OptionsProvider>
{
    private readonly IJwtService _jwtService;
    protected IRefreshTokenService RefreshTokenService { get; }
    protected HttpClient Client { get; }

    protected string Authorize(User user)
    {
        var jwt = _jwtService.CreateToken(user);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        
        return jwt;
    }

    protected ControllerTestBase(OptionsProvider optionsProvider)
    {
        var options = optionsProvider.Get<AuthOptions>("auth");
        _jwtService = new JwtService(new OptionsWrapper<AuthOptions>(options), new Clock());
        RefreshTokenService = new RefreshTokenService(new OptionsWrapper<AuthOptions>(options), new Clock());
        
        var app = new TeamManagerTestApp(ConfigureServices);
        Client = app.Client;
    }

    protected virtual void ConfigureServices(IServiceCollection serviceCollection)
    {
        
    }
}