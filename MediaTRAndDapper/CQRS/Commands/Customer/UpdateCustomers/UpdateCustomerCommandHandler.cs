using MediaTRAndDapper.Common.ICommand;
using Platform.Api.Database.Repositories.Abstract;
using static FastEndpoints.Ep;

namespace MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers
{
    public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository) : ICommandHandler<UpdateCustomerCommand>
    {
        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {



            DateTime? createdAt = request.CreatedAt ?? DateTime.Now;


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
                RefreshTokenExpiry = request.RefreshTokenExpiry,
                Orders = request.OrdersId.Select(orderId => new Models.Order { Id = orderId }).ToList()
            };

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var (newPasswordHash, newSalt) = PasswordHelper.HashPassword(request.NewPassword);
                // Hash ve salt'ı customer nesnesine ata
                customer.PasswordHash = newPasswordHash;
                customer.PasswordSalt = newSalt;
            }
            await customerRepository.UpdateAsync(customer);
        }
    }
}
