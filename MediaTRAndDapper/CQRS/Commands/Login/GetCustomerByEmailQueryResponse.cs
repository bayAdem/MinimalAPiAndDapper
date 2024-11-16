namespace MediaTRAndDapper.CQRS.Commands.Login;

public sealed record GetCustomerByEmailQueryResponse(int Id,
                                         string Email,
                                         IReadOnlyCollection<byte> PasswordHash,
                                         IReadOnlyCollection<byte> PasswordSalt,
                                         bool Active,
                                         bool Deleted)
{
}
