﻿using BusinessLogic.Abstractions;
using System.Security.Cryptography;

namespace Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SALT_SIZE = 128 / 8;
        private const int KEY_SIZE = 256 / 8;
        private const int ITERATIONS = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private static char _Delimiter = ';';

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _hashAlgorithmName, KEY_SIZE);

            return string.Join(_Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(_Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, ITERATIONS, _hashAlgorithmName, KEY_SIZE);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}