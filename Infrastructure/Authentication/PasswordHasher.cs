using BusinessLogic.Abstractions;
using System.Security.Cryptography;

namespace Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _SaltSize = 128 / 8;
        private const int _KeySize = 256 / 8;
        private const int _Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private static char _Delimiter = ';';
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(_SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _Iterations, _hashAlgorithmName, _KeySize);

            return string.Join(_Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(_Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, _Iterations, _hashAlgorithmName, _KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}