namespace EventEaseVMS.Models
{
    public class BookingDetailViewModel
    {
        public int BookingId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string VenueName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string VenueLocation { get; set; } = string.Empty;
        public int VenueCapacity { get; set; }
        public string? EventDescription { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int GuestCount { get; set; }
        public string? VenueImageUrl { get; set; }
    }
}

