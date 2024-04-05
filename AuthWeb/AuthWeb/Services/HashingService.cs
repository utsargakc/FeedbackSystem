using NuGet.Common;
using System.Security.Cryptography;
using System.Text;

namespace AuthWeb.Services
{
    public class HashingService : IHashingService
    {
        public string HashInput(string input)
        {
            SHA1 hash = SHA1.Create();
            var inputBytes = Encoding.Default.GetBytes(input);
            var hashedInput = hash.ComputeHash(inputBytes);

            return Convert.ToHexString(hashedInput);
        }
        
    }
}
