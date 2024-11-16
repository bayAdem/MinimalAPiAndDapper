using MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

namespace MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;

public static class AddCustomerMapping
{
    public static AddCustomerCommand AddCustomerCommandMapping(this AddCustomerRequest request, int Id)
    {
        ArgumentNullException.ThrowIfNull(request);

        var (passwordHash, passwordSalt) = PasswordHelper.HashPassword(request.Password);

        return new AddCustomerCommand(
            request.FullName,
            request.Email,
            request.PhoneNumber,
            passwordHash,
            passwordSalt,
            request.RefreshTokenExpiry,
            request.Address,
            request.Active,
            request.Deleted,
            request.RefreshToken,
            request.RefreshTokenExpiry,
            request.CreatedAt
        );
    }
}