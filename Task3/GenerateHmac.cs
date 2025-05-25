using System.Security.Cryptography;
using System.Text;

namespace Task3;

public class GenerateHmac
{
    public static string CalculateString(byte[] key,  int number)
    {
        using var hmac = new HMACSHA3_256(key);
        var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(number.ToString()));
        
        return BitConverter.ToString(signature).Replace("-", string.Empty);
    }
}