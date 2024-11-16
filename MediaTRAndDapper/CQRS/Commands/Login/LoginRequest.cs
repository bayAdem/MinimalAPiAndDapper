namespace MediaTRAndDapper.CQRS.Commands.Login
{
    public sealed record LoginRequest(string Email, string Password)
    {
    }
}
