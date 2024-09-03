using System.Security.Cryptography;

namespace UserManagement.Application.Utils
{
    public static class PasswordHasher
    {
        public static byte[] GenerateSalt(int size = 16)
        {
            var salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return Convert.ToBase64String(rfc2898.GetBytes(32));
            }
        }

        public static bool VerifyPassword(string password, byte[] salt, string hash)
        {
            var hashToVerify = HashPassword(password, salt);
            return hash == hashToVerify;
        }
    }
}
