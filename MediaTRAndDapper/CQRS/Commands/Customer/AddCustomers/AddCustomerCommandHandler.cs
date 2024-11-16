using MediaTRAndDapper.Common.ICommand;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;

public class AddCustomerCommandHandler(ICustomerRepository customerRepository) : ICommand<AddCustomerCommand>
{

    public async Task HandleAsync(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Models.Customer(
                                    request.Email,
                                    request.PhoneNumber,
                                    [.. request.PasswordHash],
                                    [.. request.PasswordSalt]
                                    )
        {
            LastPasswordChange = request.LastPasswordChange,
            FullName = request.FullName,
            Address = request.Address,
            Active = request.Active,
            Deleted = request.Deleted,
            CreatedAt = request.CreatedAt,
            RefreshToken = request.RefreshToken,
            RefreshTokenExpiry = request.RefreshTokenExpiry
        };
        await customerRepository.AddAsync(customer);
    }
}
