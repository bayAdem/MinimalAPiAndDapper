using MediaTRAndDapper.Common.IQuery;

namespace MediaTRAndDapper.CQRS.Commands.Login;

public sealed record GetCustomerByEmailQuery(string Email) : IQuery<GetCustomerByEmailQueryResponse?>
{
}
