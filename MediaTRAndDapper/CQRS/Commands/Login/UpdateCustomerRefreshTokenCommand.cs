using MediaTRAndDapper.Common.ICommand;

namespace MediaTRAndDapper.CQRS.Commands.Login;

public sealed record UpdateCustomerRefreshTokenCommand(int Id, string RefreshToken, DateTime RegreshTokenExpiry) : ICommand;
