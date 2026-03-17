using System.ComponentModel.DataAnnotations;

namespace EventEaseVMS.Models
{
    public class EventType
    {
        [Key]
        public int EventTypeId { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public string TypeName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}