using EventEaseVMS.Data;
using EventEaseVMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEaseVMS.Controllers
{
    public class BookingController : Controller
    {
        private readonly EventEaseDbContext _context;

        public BookingController(EventEaseDbContext context)
        {
            _context = context;
        }

        // SEARCH & FILTER
        public async Task<IActionResult> Index(string search, int? venueId, int? status)
        {
            var query = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.CustomerName.Contains(search) ||
                    b.Venue.VenueName.Contains(search));
            }

            if (venueId.HasValue)
            {
                query = query.Where(b => b.VenueId == venueId);
            }

            if (status.HasValue)
            {
                query = query.Where(b => (int)b.Status == status);
            }

            ViewBag.Venues = new SelectList(
                _context.Venues.Where(v => v.IsActive),
                "VenueId",
                "VenueName");

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentVenue = venueId;
            ViewBag.CurrentStatus = status;

            var bookings = await query
                .OrderByDescending(b => b.EventDate)
                .ToListAsync();

            return View(bookings);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // CREATE BOOKING
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                bool conflict = await _context.Bookings.AnyAsync(b =>
                    b.VenueId == booking.VenueId &&
                    b.EventDate == booking.EventDate &&
                    b.Status != BookingStatus.Cancelled &&
                    b.StartTime < booking.EndTime &&
                    b.EndTime > booking.StartTime);

                if (conflict)
                {
                    ModelState.AddModelError("",
                        "This venue is already booked for the selected date and time.");

                    PopulateDropdowns(booking);
                    return View(booking);
                }

                booking.CreatedDate = DateTime.Now;
                booking.Status = BookingStatus.Pending;

                _context.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(booking);
            return View(booking);
        }

        // EDIT PAGE
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            PopulateDropdowns(booking);

            return View(booking);
        }

        // UPDATE BOOKING
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.BookingId)
                return NotFound();

            if (ModelState.IsValid)
            {
                bool conflict = await _context.Bookings.AnyAsync(b =>
                    b.BookingId != booking.BookingId &&
                    b.VenueId == booking.VenueId &&
                    b.EventDate == booking.EventDate &&
                    b.Status != BookingStatus.Cancelled &&
                    b.StartTime < booking.EndTime &&
                    b.EndTime > booking.StartTime);

                if (conflict)
                {
                    ModelState.AddModelError("",
                        "This venue is already booked for the selected date and time.");

                    PopulateDropdowns(booking);
                    return View(booking);
                }

                _context.Update(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(booking);
            return View(booking);
        }

        // DELETE PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // DELETE CONFIRM
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // DROPDOWNS
        private void PopulateDropdowns(Booking? booking = null)
        {
            ViewBag.VenueId = new SelectList(
                _context.Venues.Where(v => v.IsActive),
                "VenueId",
                "VenueName",
                booking?.VenueId);

            ViewBag.EventTypeId = new SelectList(
                _context.EventTypes,
                "EventTypeId",
                "TypeName",
                booking?.EventTypeId);
        }
    }
}