namespace MediaTRAndDapper.CQRS.Commands.Login;

public sealed record LoginResponse(LoginResponseStatus Status,
                                   LoginResponseErrorType LoginResponseErrorType = LoginResponseErrorType.None,
                                   string? Token = null,
                                   string RefreshToken = null)
{

}
