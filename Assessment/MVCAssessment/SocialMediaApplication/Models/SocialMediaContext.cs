using Microsoft.EntityFrameworkCore;

namespace SocialMediaApplication.Models
{
    public class SocialMediaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public SocialMediaContext(DbContextOptions options) : base(options) { }
    }
}
