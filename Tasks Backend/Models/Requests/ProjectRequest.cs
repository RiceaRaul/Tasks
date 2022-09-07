namespace Tasks_Backend.Models.Requests
{
    public class ProjectRequest
    {
        public string ProjectName { get; set; }

        public int ProjectOwner { get; set; }

        public int TeamId { get; set; } = 0;
    }
}
