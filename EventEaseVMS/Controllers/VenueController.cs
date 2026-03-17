using Microsoft.AspNetCore.Mvc;
using EventEaseVMS.Data;
using EventEaseVMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEaseVMS.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDbContext _context;

        public VenueController(EventEaseDbContext context)
        {
            _context = context;
        }

        // LIST ALL VENUES
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        // SHOW CREATE PAGE
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
        }

        // SHOW EDIT PAGE
        public async Task<IActionResult> Edit(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
                return NotFound();

            ViewBag.VenueNames = Venue.SouthAfricanVenues;

            return View(venue);
        }

        // UPDATE VENUE
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
        }

        // DELETE CONFIRMATION PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // DELETE VENUE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue != null)
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}