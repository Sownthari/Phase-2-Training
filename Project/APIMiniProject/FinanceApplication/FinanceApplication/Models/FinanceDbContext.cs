using Microsoft.EntityFrameworkCore;

namespace FinanceApplication.Models
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<Management> Managements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

    }
}
