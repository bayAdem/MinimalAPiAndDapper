using MediaTRAndDapper.Common.Confings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MediaTRAndDapper.Services
{
    public class AuthenticationService(IOptions<JwtOptions> jwtOptions) : IAuthenticationService
    {
        private const int REFRESH_TOKEN_EXPIRY_MONTH = 1;// token süresi

        public static int REFRESH_TOKEN_EXPIRY_MONTH1 => REFRESH_TOKEN_EXPIRY_MONTH;

        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];// 16 bayt uzunluğunda bir dizi oluşturuluyor.
            using var rng = RandomNumberGenerator.Create(); // random sayı üretme
            rng.GetBytes(salt);
            return salt;
        }

        #region Şifre Hash'leme

        public static byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA256(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }


        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            var computedHash = HashPassword(password, storedSalt);
            return computedHash.SequenceEqual(storedHash);
        }
        #endregion

        public TokenResult GetTokenResult(int userId, string email)
        {
            ArgumentNullException.ThrowIfNull(userId, nameof(userId));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            if (userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var token = GenerateJwtToken(userId, email);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddMonths(REFRESH_TOKEN_EXPIRY_MONTH1);

            return new TokenResult(token, refreshToken, refreshTokenExpiry);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //tokn üretme
        private string GenerateJwtToken(int userId, string email)
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _jwtOptions.Issur,
                                             audience: _jwtOptions.Audience,
                                             claims: claims,
                                             notBefore: DateTime.UtcNow,
                                             expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOptions.Duration)),
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
