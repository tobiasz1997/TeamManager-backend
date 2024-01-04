using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManger.Common.Extensions.Cors;

public static class CorsExtension
{
    private const string CorsName = "AllowAll";
    
    public static IServiceCollection AddCorsExtension(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsName, builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        return services;
    }

    public static WebApplication UseCorsExtension(this WebApplication app)
    {
        app.UseCors(CorsName);
        return app;
    }
}