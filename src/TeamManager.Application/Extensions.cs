using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManager.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssemblyCommands = typeof(ICommandHandler<>).Assembly;
        var applicationAssemblyQueries = typeof(IRequestHandler<>).Assembly;

        services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });
        
        services.Scan(s =>
            s.FromAssemblies(applicationAssemblyCommands)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        
        services.Scan(s =>
            s.FromAssemblies(applicationAssemblyQueries)
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}