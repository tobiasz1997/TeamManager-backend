using MediatR;

namespace TeamManager.Common.MediatR.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
    
}