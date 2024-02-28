using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models
{
    public class OrganisationContext : IdentityDbContext<IdentityUser> 
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