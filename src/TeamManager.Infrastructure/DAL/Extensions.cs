using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Core.Assignment.Repositories;
using TeamManager.Core.User.Repositories;
using TeamManager.Infrastructure.DAL.Decorators;
using TeamManager.Infrastructure.DAL.Repositories;

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
        service.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        service.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        service.AddHostedService<DatabaseInitializer>();

        return service;
    }
}