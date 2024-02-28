using System.Text.Json.Serialization;

namespace CourseWork.Models
{
    public class StaffSkill
    {
        public int? StaffSkillId { get; set; }
        public int? StaffId { get; set; }
        public string? SkillId { get; set; }
        
        [JsonIgnore]
        public Staff? Staff { get; set; }

        [JsonIgnore]
        public Skill? Skill { get; set; }
    }
}