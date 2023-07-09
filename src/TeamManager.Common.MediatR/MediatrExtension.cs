using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManager.Common.MediatR;

public static class MediatrExtension
{
    public static IServiceCollection AddMediatrExtension(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}