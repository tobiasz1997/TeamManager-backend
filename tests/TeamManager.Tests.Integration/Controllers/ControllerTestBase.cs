using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TeamManager.Application.Shared.Services;
using TeamManager.Infrastructure;
using TeamManager.Infrastructure.Shared.Auth;
using TeamManager.Infrastructure.Shared.Time;
using Xunit;

namespace TeamManager.Tests.Integration.Controllers;

[Collection("api")]
public abstract class ControllerTestBase : IClassFixture<OptionsProvider>
{
    private readonly IJwtService _jwtService;
    protected IRefreshTokenService refreshTokenService { get; }
    protected HttpClient Client { get; }

    protected string Authorize(Guid userId, string email)
    {
        var jwt = _jwtService.CreateToken(userId, email);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        
        return jwt;
    }
    
    public ControllerTestBase(OptionsProvider optionsProvider)
    {
        var options = optionsProvider.Get<AuthOptions>("auth");
        _jwtService = new JwtService(new OptionsWrapper<AuthOptions>(options), new Clock());
        refreshTokenService = new RefreshTokenService(new OptionsWrapper<AuthOptions>(options), new Clock());
        
        var app = new TeamManagerTestApp(ConfigureServices);
        Client = app.Client;
    }

    protected virtual void ConfigureServices(IServiceCollection serviceCollection)
    {
        
    }
}