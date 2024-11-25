namespace MediaTRAndDapper.CQRS.Commands.Login;

public enum LoginResponseErrorType
{
    None,
    InvalidCredentials,
    AccountNotActive,
    AccountDeleted,
}
