using MediatR;

namespace MediaTRAndDapper.Common.IQuery;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
