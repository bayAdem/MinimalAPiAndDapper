using MediaTRAndDapper.Models;

namespace MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;

public sealed record AddCustomerRequest(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password,
    DateTime? LastPasswordChange,
    string Address,
    bool Active,
    bool Deleted,
    string RefreshToken,
    DateTime? RefreshTokenExpiry,
    DateTime? CreatedAt
);
