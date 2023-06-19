using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.User.Models;
using TeamManager.Infrastructure.DAL;
using TeamManager.Infrastructure.Shared.Auth;
using TeamManager.Infrastructure.Shared.Exceptions;
using TeamManager.Infrastructure.Shared.Security;
using TeamManager.Infrastructure.Shared.Time;
using TeamManger.Common.Extensions.Swagger;

namespace TeamManager.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();
        
        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordService, PasswordService>();

        services.AddAuth(configuration);
        services.AddHttpContextAccessor();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerExtension();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwaggerExtension();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}