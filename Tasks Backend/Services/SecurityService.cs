
using System.Security.Cryptography;
using System.Text;
using Tasks_Backend.Services.Interfaces;


namespace Tasks_Backend.Services;

public class SecurityService : ISecurityService
{
    public async Task<string> Encrypt(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())  
        {  
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();  
            for (int i = 0; i < bytes.Length; i++)  
            {  
                builder.Append(bytes[i].ToString("x2"));  
            }  
            return builder.ToString();  
        } ;
    }

    public async Task<Boolean> Verify(string rawData,string verifyData)
    {
        try
        {
            var password = Encrypt(rawData).Result;
            return password == verifyData;
        }
        catch
        {
            return false;
        }
        return false;
    }

    public string GenerateSecret()
    {
        Random random = new Random();
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()_";
        return new string(Enumerable.Repeat(characters, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}