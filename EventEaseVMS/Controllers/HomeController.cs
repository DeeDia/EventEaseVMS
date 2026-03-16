using EventEaseVMS.Models;
using EventaseVMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventEaseVMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Stats for the dashboard
            ViewBag.TotalVenues = await _context.Venues.CountAsync(v => v.IsActive);
            ViewBag.TotalBookings = await _context.Bookings.CountAsync();
            ViewBag.PendingBookings = await _context.Bookings.CountAsync(b => b.Status == BookingStatus.Pending);
            ViewBag.ConfirmedBookings = await _context.Bookings.CountAsync(b => b.Status == BookingStatus.Confirmed);

            // Upcoming bookings — next 5
            ViewBag.UpcomingBookings = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.EventType)
                .Where(b => b.EventDate >= DateTime.Today && b.Status != BookingStatus.Cancelled)
                .OrderBy(b => b.EventDate)
                .Take(5)
                .ToListAsync();

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
