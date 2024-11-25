using MediatR;

namespace MediaTRAndDapper.Common.ICommand;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
