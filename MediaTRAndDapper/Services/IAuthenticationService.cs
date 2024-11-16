namespace MediaTRAndDapper.Services;

public interface IAuthenticationService
{
    TokenResult GetTokenResult(int userId, string email);
}
