using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models
{
    public class OrganisationContext : DbContext
    {
        public OrganisationContext(DbContextOptions<OrganisationContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffSkill> StaffSkills { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}