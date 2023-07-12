using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Assignments.Repositories;
using TeamManager.Core.Timers.Repositories;
using TeamManager.Core.Users.Repositories;
using TeamManager.Infrastructure.DAL.Decorators;
using TeamManager.Infrastructure.DAL.Repositories.Assignments;
using TeamManager.Infrastructure.DAL.Repositories.Timers;
using TeamManager.Infrastructure.DAL.Repositories.Users;

namespace TeamManager.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "database";
    
    public static IServiceCollection AddPostgres(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);

        service.AddDbContext<TeamManagerDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        service.AddScoped<IAssignmentRepositoryQueries, PostgresAssignmentRepositoryQueries>();
        service.AddScoped<IAssignmentRepositoryCommands, PostgresAssignmentRepositoryCommands>();
        service.AddScoped<IUserRepositoryCommands, PostgresUserRepositoryCommands>();
        service.AddScoped<IUserRepositoryQueries, PostgresUserRepositoryQueries>();
        service.AddScoped<IRefreshTokenRepositoryCommands, PostgresRefreshTokenRepositoryCommands>();
        service.AddScoped<IProjectRepositoryCommand, PostgresProjectRepositoryCommands>();
        service.AddScoped<IProjectRepositoryQueries, PostgresProjectRepositoryQueries>();
        service.AddScoped<ITimerRepositoryCommands, PostgresTimerRepositoryCommands>();
        service.AddScoped<ITimerRepositoryQueries, PostgresTimerRepositoryQueries>();
        service.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        service.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        service.AddHostedService<DatabaseInitializer>();

        return service;
    }
}