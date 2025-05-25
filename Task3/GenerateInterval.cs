using System.Security.Cryptography;

namespace Task3;

public class GenerateInterval
{
    public static SignedData Generate(int min, int max)
    {
        var number = RandomNumberGenerator.GetInt32(min, max + 1);
        var key = GenerateSecretKey.GenerateKey();
        
        var data = new SignedData()
        {
            Hmac = GenerateHmac.CalculateString(key, number),
            Number = number,
            SecretKey = key
        };
        return data;
    }
}