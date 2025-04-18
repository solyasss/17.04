using System.Security.Cryptography;
using System.Text;

namespace _17._04.Services
{
    public class hash_service : ihash_service
    {
        public string generate_salt()
        {
            byte[] salt_buf = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt_buf);
            StringBuilder sb = new StringBuilder(salt_buf.Length * 2);
            foreach (var b in salt_buf) sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }

        public string compute_hash(string salt, string password)
        {
            byte[] pwd_bytes = Encoding.Unicode.GetBytes(salt + password);
            byte[] hash_bytes = SHA256.HashData(pwd_bytes);
            StringBuilder sb = new StringBuilder(hash_bytes.Length * 2);
            foreach (var b in hash_bytes) sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }
    }
}