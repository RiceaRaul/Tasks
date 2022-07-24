namespace Domain.Models;

public class Team:BaseEntity
{
     public string TeamName { get; set; }
     public int TeamOwnerId { get; set; }
     public User TeamOwner { get; set; }
     public List<UserTeam> TeamMembers { get; set; }
     public List<Project> TeamProjects { get; set; }
}