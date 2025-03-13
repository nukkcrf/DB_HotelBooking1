using Microsoft.EntityFrameworkCore;
using DB_HotelBooking1.Models;

namespace DB_HotelBooking1.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-1VJGJ8I;Database=HotelDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomType = "Single" ,ExtraBeds = 0 },
                new Room { Id = 2, RoomType = "Double" ,ExtraBeds = 2 },
                new Room { Id = 3, RoomType = "Double" ,ExtraBeds = 2 },
                new Room { Id = 4, RoomType = "Single",  ExtraBeds = 0 }
            );

            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new Guest { Id = 2, Name = "Bob", Email = "bob@example.com" },
                new Guest { Id = 3, Name = "Charlie", Email = "charlie@example.com" },
                new Guest { Id = 4, Name = "Diana", Email = "diana@example.com" }
            );
        }
    }
}
