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
    public static IServiceCollection AddPostgres(this IServiceCollection service, IConfiguration configuration)
    {
        // var section = configuration.GetSection("database");
        // service.Configure<PostgresOptions>(section);
        // var options = section.GetOptions<PostgresOptions>("database");


        service.AddDbContext<TeamManagerDbContext>(x => x.UseNpgsql("Host=localhost;Database=TeamManager;Username=postgres;Password=nicepassword"));
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