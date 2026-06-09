using EventEaseVMS.Data;
using EventEaseVMS.EEVServices;
using EventEaseVMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseVMS.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDbContext _context;
        private readonly BlobStorageServices _blobService;

        public VenueController(EventEaseDbContext context, BlobStorageServices blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // GET /Venue — list all venues
        public async Task<IActionResult> Index(string search)
        {
            var query = _context.Venues
                .Include(v => v.Bookings)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(v =>
                    v.VenueName.Contains(search) ||
                    v.Location.Contains(search));

            ViewBag.CurrentSearch = search;
            return View(await query.OrderBy(v => v.VenueName).ToListAsync());
        }

        // GET /Venue/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        // GET /Venue/Create
        public IActionResult Create()
        {
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View();
        }

        // POST /Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                if (venue.ImageFile != null && venue.ImageFile.Length > 0)
                    venue.ImageUrl = await _blobService.UploadImageAsync(venue.ImageFile);

                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }

        // GET /Venue/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }

        // POST /Venue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (ModelState.IsValid)
            {
                if (venue.ImageFile != null && venue.ImageFile.Length > 0)
                {
                    if (!string.IsNullOrEmpty(venue.ImageUrl))
                        await _blobService.DeleteImageAsync(venue.ImageUrl);
                    venue.ImageUrl = await _blobService.UploadImageAsync(venue.ImageFile);
                }
                _context.Update(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }

        // GET /Venue/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Bookings)
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) return NotFound();

            ViewBag.HasActiveBookings = venue.Bookings != null &&
                venue.Bookings.Any(b => b.Status != BookingStatus.Cancelled);

            return View(venue);
        }

        // POST /Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Bookings)
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) return NotFound();

            bool active = venue.Bookings != null &&
                venue.Bookings.Any(b => b.Status != BookingStatus.Cancelled);

            if (active)
            {
                TempData["Error"] = "Cannot delete this venue — it has active bookings. " +
                                    "Cancel all associated bookings first.";
                return RedirectToAction(nameof(Index));
            }

            // Delete image from blob storage if it exists
            if (!string.IsNullOrEmpty(venue.ImageUrl))
                await _blobService.DeleteImageAsync(venue.ImageUrl);

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}