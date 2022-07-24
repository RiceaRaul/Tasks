using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        
        public string? Password { get; set; }

        public string? Email { get; set; }
        public string? Secret { get; set; }

        public List<Project>? Projects { get; set; }

        public List<UserTeam> Teams;
    }
}
