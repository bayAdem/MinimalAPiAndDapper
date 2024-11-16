namespace MediaTRAndDapper.Services;

public sealed record TokenResult(string Token, string RefreshToken, DateTime RefreshTokenExpiry);
