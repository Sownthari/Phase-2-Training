using Microsoft.EntityFrameworkCore;

namespace DessertAPIRepo.Models
{
    public class DessertContext : DbContext
    {
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Flavour> Flavours { get; set; }
        public DbSet<DessertFlavour> DessertFlavours { get; set; }
        public DessertContext(DbContextOptions<DessertContext> options) : base(options) { }

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
