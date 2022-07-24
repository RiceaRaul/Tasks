using Domain.Models;

namespace Tasks_Backend.Models.Requests;


public class AuthentificationUserResponse
{
    public string Username { get; init; }
}
public class AuthentificationResponse
{
    public string AuthToken { get; init; }
    public AuthentificationUserResponse UserData { get; init; }
}
