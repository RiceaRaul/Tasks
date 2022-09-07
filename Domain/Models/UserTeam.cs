namespace Domain.Models;

public class UserTeam : BaseEntity
{
    public  int? UserId { get; set; }
    
    public User? User { get; set; }
        
    public int? TeamId { get; set; }

    public Team? UserTeams { get; set; } 
    
}