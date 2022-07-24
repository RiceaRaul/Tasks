namespace Domain.Models
{
    public  class Project : BaseEntity
    {
        public string? ProjectName { get; set; }

        public User? Owner { get; set; }

        public int? OwnerId { get; set; }
        
        public int TeamId  { get; set; }
        
        public Team Team  { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
