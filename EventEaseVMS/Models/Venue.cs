using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventEaseVMS.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Venue Name")]
        [StringLength(100)]
        public string VenueName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int Capacity { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // Navigation property
        public ICollection<Booking>? Bookings { get; set; }

        public static List<SelectListItem> SouthAfricanVenues =>
            new List<SelectListItem>
            {
                new SelectListItem { Value = "Shepstone Gardens", Text = "Shepstone Gardens – Johannesburg" },
                new SelectListItem { Value = "Glenburn Lodge", Text = "Glenburn Lodge – Muldersdrift" },
                new SelectListItem { Value = "Avianto Estate", Text = "Avianto Estate – Muldersdrift" },
                new SelectListItem { Value = "Sandton Convention Centre", Text = "Sandton Convention Centre – Sandton" },
                new SelectListItem { Value = "Montecasino", Text = "Montecasino – Fourways, Johannesburg" }
            };
    }
}