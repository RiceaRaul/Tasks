using System.ComponentModel.DataAnnotations;

namespace Tasks_Backend.Models.Requests;

public class AuthentificationRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}

public class AuthentificationRegisterRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
    
}