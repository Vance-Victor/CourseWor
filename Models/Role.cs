namespace CourseWork.Models
{
    public class Role
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Responsibility { get; set; }
        public Project? Project { get; set; }
        public Staff? Staff { get; set; }
    }
}