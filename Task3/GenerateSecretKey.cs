using System.Security.Cryptography;

namespace Task3;

public class GenerateSecretKey
{
    public static byte[] GenerateKey()
    {
        var key = new byte[32];
        RandomNumberGenerator.Fill(key);
        return key;
    }
}