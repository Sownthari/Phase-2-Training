
using Microsoft.EntityFrameworkCore;

namespace MVCHotel.Models
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source = PTSQLTESTDB01;database = Sownthari;integrated security=true;trustservercertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .HasData(new Hotel() { HotelId = 1, HotelName = "Feathers" },
                new Hotel() { HotelId = 2, HotelName = "Taj" });

            modelBuilder.Entity<Room>()
                .HasData(new Room() { RoomId = 1, RoomNo = "A101", RoomType = "AC", Price = 20000, HotelId = 1 });

            modelBuilder.Entity<Room>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Hotel>()
                .HasMany(r => r.Rooms)
                .WithOne(h => h.Hotel)
                .HasForeignKey(c => c.HotelId);
        }
    }
}
