using PocketGym.Application.Core.Dtos;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PocketGym.Application.Extensions
{
    public static class AuthenticationExtension
    {
        public static void Authenticate(this LoginDto login, byte[] hash, byte[] salt)
        {
            if (!IsValid(login.Password, hash, salt))
            {
                throw new UnauthorizedAccessException();
            }
        }

        public static (byte[] PasswordHash, byte[] PasswordSalt) Encrypt(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
            }
        }

        private static bool IsValid(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
