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
        { _context = context; _blobService = blobService; }


        // LIST ALL VENUES
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        /* SHOW CREATE PAGE
        public IActionResult Create()
        {
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View();
        }

        // SAVE NEW VENUE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }*/

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
            ViewBag.VenueNames = Venue.SouthAfricanVenues; return View(venue);
        }




        /* SHOW EDIT PAGE
        public async Task<IActionResult> Edit(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
                return NotFound();

            ViewBag.VenueNames = Venue.SouthAfricanVenues;

            return View(venue);
        }*/

        // POST /Venue/Edit — replaces old blob if a new image is uploaded
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
                _context.Update(venue); await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VenueNames = Venue.SouthAfricanVenues; return View(venue);
        }


        /* UPDATE VENUE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (id != venue.VenueId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Venues.Any(v => v.VenueId == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.VenueNames = Venue.SouthAfricanVenues;

            return View(venue);
        }*/

        // DELETE CONFIRMATION PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues //to protect Delete Get action, we check if there are
                                              //active bookings for the venue. If there are, we can show a
                                              //warning message in the view and prevent deletion.
                .Include(v => v.Bookings)
        .FirstOrDefaultAsync(v => v.VenueId == id);
            if (venue == null) return NotFound();
            ViewBag.HasActiveBookings = venue.Bookings != null &&
                venue.Bookings.Any(b => b.Status != BookingStatus.Cancelled);

            return View(venue);
        }

        // DELETE VENUE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.Include(v => v.Bookings)  //protect Delete Post action, we check again for
                                                                        //active bookings before deletion. This is a safety
                                                                        //net in case someone tries to bypass the confirmation page.
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
            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}