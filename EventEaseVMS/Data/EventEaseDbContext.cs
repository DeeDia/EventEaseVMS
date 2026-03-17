using Microsoft.EntityFrameworkCore;
using EventEaseVMS.Models;

namespace EventEaseVMS.Data
{
    public class EventEaseDbContext : DbContext
    {
        public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<EventType> EventTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking -> Venue relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VenueId);

            // Booking -> EventType relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.EventType)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventTypeId);
        }
    }
}