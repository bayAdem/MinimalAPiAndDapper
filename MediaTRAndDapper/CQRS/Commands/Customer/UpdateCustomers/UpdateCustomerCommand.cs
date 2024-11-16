using MediaTRAndDapper.Common.ICommand;
using MediaTRAndDapper.Models;

namespace MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

public sealed record UpdateCustomerCommand(int Id,
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
                                            DateTime? CreatedAt,
                                            string? NewPassword,
                                           ICollection<int> OrdersId) : ICommand;
