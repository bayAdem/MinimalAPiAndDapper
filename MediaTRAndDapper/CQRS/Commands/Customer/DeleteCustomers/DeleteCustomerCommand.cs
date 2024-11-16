using MediaTRAndDapper.Common.ICommand;

namespace MediaTRAndDapper.CQRS.Commands.Customer.DeleteCustomers;

public sealed record DeleteCustomerCommand(int Id) : ICommand
{
}
