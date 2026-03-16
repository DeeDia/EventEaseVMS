using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventEaseVMS.Models

{
    public class Venue
    {
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Venue Name")]
        [StringLength(100)]
        public string VenueName { get; set; } = string.Empty;


        public string Location { get; set; } = string.Empty;

        
        public int Capacity { get; set; }

        [StringLength(500)]
        public string? Description {  get; set; }

        [Display(Name = "Image URL")]
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        //Navigation property
        public ICollection<Booking>? Bookings { get; set; }

        public static List<SelectListItem> SouthAfricanVenues =>
            new List<SelectListItem>
            {
                new SelectListItem { Value = "The Venue at Hout Bay", Text = "The Venue at Hout Bay – Cape Town" },
                new SelectListItem { Value = "Shepstone Gardens", Text = "Shepstone Gardens – Johannesburg" },
                new SelectListItem { Value = "Glenburn Lodge", Text = "Glenburn Lodge – Muldersdrift" },
                new SelectListItem { Value = "Avianto Estate", Text = "Avianto Estate – Muldersdrift" },
                new SelectListItem { Value = "Mosaic at the Owned Estate", Text = "Mosaic at the Owned Estate – Pretoria" },
                new SelectListItem { Value = "Netherwood Estate", Text = "Netherwood Estate – KwaZulu-Natal" },
                new SelectListItem { Value = "Spier Wine Farm", Text = "Spier Wine Farm – Stellenbosch" },
                new SelectListItem { Value = "Babylonstoren", Text = "Babylonstoren – Franschhoek" },
                new SelectListItem { Value = "The Oyster Box", Text = "The Oyster Box – Umhlanga" },
                new SelectListItem { Value = "Lourensford Wine Estate", Text = "Lourensford Wine Estate – Somerset West" },
                new SelectListItem { Value = "Montecasino", Text = "Montecasino – Fourways, Johannesburg" },
                new SelectListItem { Value = "The Forum at The Campus", Text = "The Forum at The Campus – Bryanston" },
                new SelectListItem { Value = "CTICC", Text = "Cape Town International Convention Centre – Cape Town" },
                new SelectListItem { Value = "Sandton Convention Centre", Text = "Sandton Convention Centre – Sandton" },
                new SelectListItem { Value = "Sun City Resort", Text = "Sun City Resort – North West" },
            };
    }
}
