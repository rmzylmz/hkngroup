using System;
using System.Security.Cryptography;

namespace HKNGroup.Core.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128-bit
        private const int KeySize = 32;  // 256-bit
        private const int Iterations = 100_000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
        private const char SegmentDelimiter = ':';

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Parola boş olamaz.", nameof(password));
            }

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, KeySize);

            return string.Join(
                SegmentDelimiter,
                Convert.ToBase64String(hash),
                Convert.ToBase64String(salt),
                Iterations,
                Algorithm.Name
            );
        }

        public bool VerifyPassword(string hashedPasswordOrPlainText, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPasswordOrPlainText) || string.IsNullOrEmpty(providedPassword))
            {
                return false;
            }

            // Geriye dönük uyumluluk: Eğer kayıt henüz hashlenmemiş düz metin ise
            if (hashedPasswordOrPlainText == providedPassword)
            {
                return true;
            }

            string[] segments = hashedPasswordOrPlainText.Split(SegmentDelimiter);
            if (segments.Length != 4)
            {
                return false;
            }

            try
            {
                byte[] hash = Convert.FromBase64String(segments[0]);
                byte[] salt = Convert.FromBase64String(segments[1]);
                int iterations = int.Parse(segments[2]);
                var algorithm = new HashAlgorithmName(segments[3]);

                byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, iterations, algorithm, hash.Length);
                return CryptographicOperations.FixedTimeEquals(hash, inputHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
