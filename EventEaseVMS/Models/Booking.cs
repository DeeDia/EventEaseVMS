using System.ComponentModel.DataAnnotations;

namespace EventEaseVMS.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        // CUSTOMER DETAILS
        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string? CustomerPhone { get; set; }

        // EVENT DETAILS
        [Required]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Number of Guests")]
        [Range(1, 10000)]
        public int GuestCount { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500)]
        public string? Notes { get; set; }

        // STATUS
        [Display(Name = "Booking Status")]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // FOREIGN KEYS
        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public int EventTypeId { get; set; }

        // NAVIGATION PROPERTIES
        public Venue? Venue { get; set; }

        public EventType? EventType { get; set; }
    }
}