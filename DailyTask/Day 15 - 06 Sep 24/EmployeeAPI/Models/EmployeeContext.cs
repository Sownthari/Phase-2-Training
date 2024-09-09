using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Models
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ValidUser> ValidUsers { get; set; }
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
    }
}
