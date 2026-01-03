using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using SchoolFees.EN.Exceptions;

namespace SchoolFees.BL.Security
{
    public static class PasswordHasher
    {
        // ============================
        //      PARÁMETROS DE SEGURIDAD
        // ============================

        private const int SaltSize = 16;          // 128 bits
        private const int HashSize = 32;          // 256 bits
        private const int MemorySize = 1 << 16;   // 64 MB
        private const int Iterations = 4;
        private const int Parallelism = 2;

        // ============================
        //  HASH DE CONTRASEÑA
        // ============================

        public static (string Hash, string Salt) HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new BusinessException("La contraseña no puede estar vacía.");

            byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = saltBytes,
                MemorySize = MemorySize,
                Iterations = Iterations,
                DegreeOfParallelism = Parallelism
            };

            byte[] hashBytes = argon2.GetBytes(HashSize);

            return (
                Convert.ToBase64String(hashBytes),
                Convert.ToBase64String(saltBytes)
            );
        }

        // ============================
        // 🔍 VERIFICACIÓN
        // ============================

        public static bool VerifyPassword(
            string password,
            string storedHash,
            string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] expectedHash = Convert.FromBase64String(storedHash);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = saltBytes,
                MemorySize = MemorySize,
                Iterations = Iterations,
                DegreeOfParallelism = Parallelism
            };

            byte[] computedHash = argon2.GetBytes(HashSize);

            return CryptographicOperations.FixedTimeEquals(
                computedHash,
                expectedHash
            );
        }
    }
}
