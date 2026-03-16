
// Controllers for the VenueController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventEaseVMS.Data;
using EventEaseVMS.Models;
using Microsoft.EntityFrameworkCore;



namespace EventEaseVMS.Controllers
{
    public class VenueController : Controller //inherit controller
    {
        private readonly EventEaseDbContext _context;
        //DbContext is injected automatically by ASP.NET
        public VenueController(EventEaseDbContext context)
        {
            _context = context;
        }

        //lIST OF ALL VENUES
        public async Task<IActionResult> Index()
        {
            var vvenus = await _context.Venues.ToListAsync();
            return View(vvenus);// passes list to Views/Venue/Index.cshtml
        }

        public IActionResult create()
        {
            // Send the SA venues dropdown list to the view
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View();
        }

        //To save each new venue to te database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // If validation fails, repopulate the dropdown before returning the view
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }

        public async Task<IActionResult> Edit(int id) //shows edit form pre-filled
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();

            // Repopulate dropdown for edit form
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Update(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VenueNames = Venue.SouthAfricanVenues;
            return View(venue);
        }

        //delete confirmation page
        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        //now the delet confirmation 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
