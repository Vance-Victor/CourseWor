using System.Text.Json.Serialization;

namespace CourseWork.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Deadline { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }
    }
}