﻿using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Application.Shared.Abstractions.Queries;

namespace TeamManager.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssemblyCommands = typeof(ICommandHandler<>).Assembly;
        var applicationAssemblyQueries = typeof(IQueryHandler<,>).Assembly;
        
        services.Scan(s =>
            s.FromAssemblies(applicationAssemblyCommands)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        
        services.Scan(s =>
            s.FromAssemblies(applicationAssemblyQueries)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        
        // services.AddScoped<ICommandHandler<CreateAssignment>, CreateAssignmentHandler>();
        // services.AddScoped<IQueryHandler<GetAssignment, AssignmentDto>, GetAssignmentHandler>();
        
        return services;
    }
}