using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EncriptacionService: IEncriptacionService
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32; // 256 bits
        private const int Iterations = 10000; // Número de iteraciones recomendado

        public string HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
                var key = pbkdf2.GetBytes(KeySize);
                return Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(key);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('$');
            if (parts.Length != 2)
            {
                return false;
            }
            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                var computedKey = pbkdf2.GetBytes(KeySize);
                for (int i = 0; i < computedKey.Length; i++)
                {
                    if (computedKey[i] != key[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
