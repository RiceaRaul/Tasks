using System.ComponentModel.DataAnnotations;

namespace Tasks_Backend.Models.Requests;

public class TaskRequest
{
    [Required]
    public string? TaskName { get; set; }
    [Required]
    public int TaskProjectId { get; set; }
    
}