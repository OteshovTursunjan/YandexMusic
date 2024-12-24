using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YandexMusic.DataAccess.Authentication
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int KeySize = 32;
        private const int IterationsCount = 10_000;

        public string Encrypt(string password, string salt)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                iterations: IterationsCount,
                hashAlgorithm: HashAlgorithmName.SHA256))
            {
                return Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }
        }

        public bool Verify(string hash, string password, string salt)
        {
            // Compare the provided hash with the one generated from the input
            return hash == Encrypt(password, salt);
        }

        public static string GenerateSalt(int size = 16)
        {
            // Generate a cryptographically secure random salt
            var saltBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }

}
