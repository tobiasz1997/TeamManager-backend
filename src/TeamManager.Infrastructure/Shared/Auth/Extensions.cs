using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Application.Shared.Services;

namespace TeamManager.Infrastructure.Shared.Auth;

internal static class Extensions
{
    private const string SectionName = "auth"; 
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AuthOptions>(SectionName);
        
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));

        services
            .AddSingleton<IJwtService, JwtService>()
            .AddSingleton<IRefreshTokenService, RefreshTokenService>()
            .AddSingleton<IAccessTokenStorage, AccessTokenStorage>()
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = options.Audience;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });

        services.AddAuthorization();

        return services;
    }
}