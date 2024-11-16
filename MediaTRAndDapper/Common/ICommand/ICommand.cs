using MediatR;
using MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;

namespace MediaTRAndDapper.Common.ICommand;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
