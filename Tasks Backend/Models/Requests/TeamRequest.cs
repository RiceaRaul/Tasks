using System.ComponentModel.DataAnnotations;

namespace Tasks_Backend.Models.Requests;

public class TeamRequest
{
    [Required]
    public string TeamName { get; set; }
    
}

public class AddUserTeamRequest
{
    [Required]
    public int TeamId { get; set; }
    
    [Required]
    public int UserId { get; set; }
}