using System.Runtime.InteropServices;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIXORA.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<TechnicianProfile> TechnicianProfiles { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceCategory>()
                .Property(s => s.BaseServiceCharge)
                .HasPrecision(10, 2);     // 10 digits total, 2 after decimal point

            modelBuilder.Entity<Booking>()
                .Property(b => b.Amount)
                .HasPrecision(10, 2);

            // One review per booking
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.BookingId)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
