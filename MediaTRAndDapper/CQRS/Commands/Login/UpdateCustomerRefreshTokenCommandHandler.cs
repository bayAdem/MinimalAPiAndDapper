using MediaTRAndDapper.Common.ICommand;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Login
{
    public class UpdateCustomerRefreshTokenCommandHandler(ICustomerRepository customerRepository) : ICommandHandler<UpdateCustomerRefreshTokenCommand>
    {


        public async Task Handle(UpdateCustomerRefreshTokenCommand request, CancellationToken ct)
        {
            await customerRepository.UpdateRefreshTokenAsync(request.Id, request.RefreshToken, request.RegreshTokenExpiry);
        }
    }
}
