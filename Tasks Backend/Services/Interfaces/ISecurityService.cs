namespace Tasks_Backend.Services.Interfaces;

public interface ISecurityService
{
    Task<string> Encrypt(string rawData);

    Task<Boolean> Verify(string rawData, string verifyData);

    string GenerateSecret();
}