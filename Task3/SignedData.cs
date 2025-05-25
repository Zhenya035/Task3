namespace Task3;

public class SignedData
{
    public string Hmac { get; set; } = string.Empty;
    public int Number { get; set; } = 0;
    
    public byte[] SecretKey { get; set; } = [];
}