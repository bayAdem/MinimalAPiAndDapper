using MediaTRAndDapper.Common.ICommand;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Customer.DeleteCustomers
{
    public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository) : ICommandHandler<DeleteCustomerCommand>
    {

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                throw new ArgumentException("Id bulunamadı");
            }
            await customerRepository.DeleteAsync(request.Id);
        }
    }
}
