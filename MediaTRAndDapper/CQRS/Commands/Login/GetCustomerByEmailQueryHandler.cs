using MediaTRAndDapper.Common.IQuery;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Login;

public class GetCustomerByEmailQueryHandler(ICustomerRepository customerRepository) : IQueryHandler<GetCustomerByEmailQuery, GetCustomerByEmailQueryResponse?>
{


    public async Task<GetCustomerByEmailQueryResponse?> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var userPhone = await customerRepository.GetCustomerEmailAsync(request.Email);

        if (userPhone == null)
        {
            return null;
        }
        return new GetCustomerByEmailQueryResponse(userPhone.Id,
                                               userPhone.Email,
                                               userPhone.PasswordHash.ToArray(),
                                               userPhone.PasswordSalt.ToArray(),
                                               userPhone.Active,
                                               userPhone.Deleted);
    }
}
