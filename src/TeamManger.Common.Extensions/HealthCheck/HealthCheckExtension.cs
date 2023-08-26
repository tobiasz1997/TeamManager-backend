using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManger.Common.Extensions.HealthCheck;

public static class HealthCheckExtension
{
    public static IServiceCollection AddHealthCheckExtension(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: add db health check
        services.AddHealthChecks();
        return services;
    }

    public static WebApplication UseHealthCheckExtension(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return app;
    }
    
}