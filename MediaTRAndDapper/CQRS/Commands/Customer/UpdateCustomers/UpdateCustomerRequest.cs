namespace MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

public sealed record UpdateCustomerRequest(int Id,
                                            string FullName,
                                            string Email,
                                            string PhoneNumber,
                                            string Address,
                                            DateTime? LastPasswordChange,
                                            string? RefreshToken,
                                            DateTime? RefreshTokenExpiry,
                                            bool? Active,
                                            bool? Deleted,
                                            byte[] PasswordHash,
                                            byte[] PasswordSalt,
                                            DateTime? CreatedAt,
                                            string? NewPassword,
                                            ICollection<int> OrdersId);

