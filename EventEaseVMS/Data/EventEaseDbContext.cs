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

        public DbSet<BookingDetailViewModel> BookingDetails { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking -> Venue relationship
            /* modelBuilder.Entity<Booking>()
                 .HasOne(b => b.Venue)
                 .WithMany(v => v.Bookings)
                 .HasForeignKey(b => b.VenueId);

             // Booking -> EventType relationship
             modelBuilder.Entity<Booking>()
                 .HasOne(b => b.EventType)
                 .WithMany(e => e.Bookings)
                 .HasForeignKey(b => b.EventTypeId);*/

            modelBuilder.Entity<Venue>().HasData(
            new Venue { VenueId = 1, VenueName = "Shepstone Gardens", Location = "Johannesburg", Capacity = 200, IsActive = true },
            new Venue { VenueId = 2, VenueName = "Montecasino", Location = "Fourways", Capacity = 500, IsActive = true },
            new Venue { VenueId = 3, VenueName = "Sandton Convention Centre", Location = "Sandton", Capacity = 1000, IsActive = true },
            new Venue { VenueId = 4, VenueName = "Glenburn Lodge", Location = "Muldersdrift", Capacity = 150, IsActive = true },
            new Venue { VenueId = 5, VenueName = "Avianto Estate", Location = "Muldersdrift", Capacity = 300, IsActive = true },
            new Venue { VenueId = 6, VenueName = "The Forum Bryanston", Location = "Bryanston", Capacity = 250, IsActive = true },
            new Venue { VenueId = 7, VenueName = "Sun City Resort", Location = "North West", Capacity = 800, IsActive = true },
            new Venue { VenueId = 8, VenueName = "Spier Wine Farm", Location = "Stellenbosch", Capacity = 400, IsActive = true },
            new Venue { VenueId = 9, VenueName = "The Oyster Box", Location = "Umhlanga", Capacity = 180, IsActive = true },
            new Venue { VenueId = 10, VenueName = "CTICC", Location = "Cape Town", Capacity = 1200, IsActive = true }
        );

            // Seed predefined EventType categories
            modelBuilder.Entity<EventType>().HasData(
                new EventType
                {
                    EventTypeId = 1,
                    TypeName = "Conference",
                    Description = "Business conferences and professional summits"
                },
                new EventType
                {
                    EventTypeId = 2,
                    TypeName = "Wedding",
                    Description = "Wedding ceremonies and receptions"
                },
                new EventType
                {
                    EventTypeId = 3,
                    TypeName = "Concert",
                    Description = "Live music performances and shows"
                },
                new EventType
                {
                    EventTypeId = 4,
                    TypeName = "Birthday",
                    Description = "Birthday celebrations and parties"
                },
                new EventType
                {
                    EventTypeId = 5,
                    TypeName = "Corporate Function",
                    Description = "Team events, award ceremonies, launches"
                },
                new EventType
                {
                    EventTypeId = 6,
                    TypeName = "Exhibition",
                    Description = "Art shows, trade expos, displays"
                },
                new EventType
                {
                    EventTypeId = 7,
                    TypeName = "Workshop",
                    Description = "Training sessions and interactive workshops"
                },
                new EventType
                {
                    EventTypeId = 8,
                    TypeName = "Gala Dinner",
                    Description = "Formal dinners and fundraising galas"
                }
            );

            // Existing keyless view mapping
            modelBuilder.Entity<BookingDetailViewModel>()
                .ToView("vw_BookingDetails").HasNoKey();



           /* modelBuilder.Entity<EventType>().HasData(
            new EventType { EventTypeId = 1, TypeName = "Wedding" },
            new EventType { EventTypeId = 2, TypeName = "Conference" },
            new EventType { EventTypeId = 3, TypeName = "Birthday Party" },
            new EventType { EventTypeId = 4, TypeName = "Corporate Event" },
            new EventType { EventTypeId = 5, TypeName = "Concert" },
            new EventType { EventTypeId = 6, TypeName = "Graduation" },
            new EventType { EventTypeId = 7, TypeName = "Baby Shower" }
        );
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookingDetailViewModel>()
                .ToView("vw_BookingDetails")
                .HasNoKey();
           */


        }
    }
}