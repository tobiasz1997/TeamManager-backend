using Mediator;

namespace TeamManager.Infrastructure.DAL.UnitOfWorks;

internal sealed class PostgresUnitOfWork<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, ICommand
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresUnitOfWork(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) return await next(message, cancellationToken);
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var response = await next(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
    
    private static bool IsCommand() => typeof(TRequest).Namespace!.EndsWith("Commands");
}