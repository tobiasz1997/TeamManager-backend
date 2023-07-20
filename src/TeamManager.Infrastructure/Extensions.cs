using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.Users.Models;
using TeamManager.Infrastructure.DAL;
using TeamManager.Infrastructure.Shared.Auth;
using TeamManager.Infrastructure.Shared.Middlewares;
using TeamManager.Infrastructure.Shared.Security;
using TeamManager.Infrastructure.Shared.Time;
using TeamManger.Common.Extensions.Swagger;

namespace TeamManager.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<LoggingMiddleware>()
            .AddSingleton<ExceptionMiddleware>()
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordService, PasswordService>()
            .AddAuth(configuration)
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddSwaggerExtension();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwaggerExtension();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
    
    //TODO: move to core
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}