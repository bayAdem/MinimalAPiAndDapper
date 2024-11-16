namespace MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

using System.Security.Cryptography;
using System.Text;

public class PasswordHelper
{
    // Yeni bir salt oluştur
    public static byte[] GenerateSalt(int length = 32)
    {
        using var rng = new RNGCryptoServiceProvider();
        byte[] salt = new byte[length];
        rng.GetBytes(salt);
        return salt;
    }

    public static (byte[] passwordHash, byte[] salt) HashPassword(string password)
    {
        byte[] salt = GenerateSalt();
        using var sha256 = SHA256.Create();
        // Salt ve password birleştirilir ve hash'lenir
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        byte[] hash = sha256.ComputeHash(saltedPassword);
        return (hash, salt);
    }

    public static bool VerifyPassword(string inputPassword, byte[] storedHash, byte[] storedSalt, Exception argumentNullException)
    {
        if (storedHash == null || inputPassword == null || storedSalt == null)
        {
            throw argumentNullException;
        }

        var (inputHash, _) = HashPasswordWithSalt(inputPassword, storedSalt);

        return storedHash.Length == inputHash.Length && !storedHash.Where((t, i) => t != inputHash[i]).Any();
    }

    public static (byte[] hash, byte[] salt) HashPasswordWithSalt(string password, byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return (hash, salt);
    }

}
