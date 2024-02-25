namespace CourseWork.Models
{
    public class StaffSkill
    {
        public int? StaffSkillId { get; set; }
        public int? StaffId { get; set; }
        public string? SkillId { get; set; }
        
        public Staff? Staff { get; set; }
        public Skill? Skill { get; set; }
    }
}