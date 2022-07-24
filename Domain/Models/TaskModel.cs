namespace Domain.Models;

public class TaskModel : BaseEntity
{
    public string TaskName { get; set; }
    
    public int TaskProjectId { get; set; }
    
    public Project TaskProject { get; set; }
    
    public int TaskStatus { get; set; }
}