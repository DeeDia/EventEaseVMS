using EventaseVMS.Data;
using EventaseVMS.Models;
using EventEaseVMS.Data;
using EventEaseVMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EventEaseVMS.Controllers
{
    public class BookingController : Controller
    {
        private readonly EventEaseDbContext _Context;

        public BookingController(EventEaseDbContext context)
        {
            _Context = context;
        }

        //search filter
        public async Task<IActionResult> Index(string search, int? venueId, int? status)
        {
            var query = _Context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .AsQueryable();

            //search by customer name or venue name
            if (!string.IsNullOrEmpty(search))
                query = query.where(b =>
                    b.CustomerName.Contains(search) ||
                    b.Venue.VenueName.Contains(search));

            //Filter by Venue
            if (venueId.HasValue)
                query = query.Where(b => b.VenueId == venueId);

            //Filter by status
            if (status.HasValue)
                query = query.Where(b => (int)b.StatusCode == status);

            //Pass filter data to view for dropdown
            ViewBag.Venues = new SelectList(_Context.Venues.Where(v => v.IsActive), "VenueId", "VenueName");
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentVenue = venueId;
            ViewBag.CurrentVenue = status;

            var bookings = await query
                .OrderByDescending(b => b.EventDate)
                .ToListAsync();

            return View(bookings);
        }
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _Context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null) return NotFound();
            return View(booking);
        }
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                //here it will detection
                bool conflict = await _Context.Bookings.AnyAsync(b =>
                b.VenuId == booking.VenueId &&
                b.EventDate == booking.EventDate &&
                b.Status != BookingStatus.Cancelled &&
                b.StartTime < booking.EndTime &&
                b.EndTime > booking.StartTime);

                if (conflict)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date and time. Please choose a different time or venue.");
                    PopulateDropdowns(booking);
                    return View(booking);
                }
                booking.CreatedDate = DateTime.Now;
                booking.Status = BookingStatus.Pending;
                _Context.Add(booking);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(booking);
            return View(booking);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _Context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();
            PopulateDropdowns(booking);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (ModelState.IsValid)
            {
                //checking for conflict check
                bool conflict = await _Context.Bookings.AnyAsync(b =>
                    b.BookingId != booking.BookingId &&
                    b.VenueId == booking.VenueId &&
                    b.EventDate == booking.EventDate &&
                    b.Status != BookingStatuse.Canceled &&
                    b.StartTime < booking.EndTime &&
                    b.EndTime > booking.StartTime);
            }

            if (conflict)
            {
                ModelState.AddModelError("",
                    "This venue is already booked for the selected date and time.");
                PopulateDropdowns(booking);
                return View(booking);
            }

            _Context.Update(booking);
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopilationDropdowns(booking);
        Return View(booking);

    }

    public async Task<IActionResult> Delete(int id)
        {
            var booking = await _Context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _Context.Bookings.FindAsync(id);
            _Context.Bookings.Remove(booking);
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(Booking booking = null)
        {
            ViewBag.VenueId = new SelectList(
                _Context.Venues.Where(v => v.IsActive),
                "VenueId", "VenueName", booking?.VenueId);

            ViewBag.EventTypeId = new SelectList(
                _Context.EventTypes,
                "EventTypeId", "TypeName", booking?.EventTypeId);
        }

    }
}
