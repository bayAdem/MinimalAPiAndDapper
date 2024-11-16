using MediatR;

namespace MediaTRAndDapper.Common.IQuery;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
