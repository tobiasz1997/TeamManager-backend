using MediatR;
using TeamManager.Common.MediatR.Commands;

namespace TeamManager.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ExecuteAsync(() => _commandHandler.Handle(request, cancellationToken));
        return Unit.Value;
    }
}