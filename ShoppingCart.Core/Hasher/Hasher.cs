using System.Security.Cryptography;
using System.Text;

namespace ShoppingCart.Core.Hasher
{
    public class Hasher : IHasher
    {
        public string Hash(string inputString)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha256.ComputeHash(bytes);
            return Stringify(hash);
        }

        private static string Stringify(byte[] hashedBytes)
        {
            var result = new StringBuilder();

            foreach (var hash in hashedBytes)
                result.Append(hash.ToString("X2"));

            return result.ToString();
        }
    }
}