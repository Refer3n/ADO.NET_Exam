using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.DAL
{
    public class HotelContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conString = "Data Source=DESKTOP-VI7HLAA\\SQLSERVER;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(conString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RoomId);
        }

    }
}