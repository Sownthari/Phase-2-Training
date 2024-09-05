using Microsoft.EntityFrameworkCore;

namespace DessertAPI.Models
{
    public class DessertDbContext : DbContext
    {
        public DbSet<Dessert> desserts { get; set; }
        public DbSet<Flavour> flavours { get; set; }
        public DbSet<DessertFlavour> dessertFlavours { get; set; }
        public DessertDbContext(DbContextOptions<DessertDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DessertFlavour>()
                .HasKey(df => new { df.DessertId, df.FlavourId });

            modelBuilder.Entity<DessertFlavour>()
                .HasOne(df => df.dessert)
                .WithMany(d => d.DessertFlavours)
                .HasForeignKey(d => d.DessertId);

            modelBuilder.Entity<DessertFlavour>()
                .HasOne(df => df.flavour)
                .WithMany(f => f.DessertFlavours)
                .HasForeignKey(f => f.FlavourId);
        }
    }
}
