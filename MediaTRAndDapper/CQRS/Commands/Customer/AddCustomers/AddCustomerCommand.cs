namespace MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;

public sealed record AddCustomerCommand(
    string FullName,
    string Email,
    string PhoneNumber,
    byte[] PasswordHash,
    byte[] PasswordSalt,
    DateTime? LastPasswordChange,
    string Address,
    bool Active,
    bool Deleted,
    string RefreshToken,
    DateTime? RefreshTokenExpiry,
    DateTime? CreatedAt
);

