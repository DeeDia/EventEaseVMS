using EventEaseVMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseVMS.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly EventEaseDbContext _context;
        public BookingDetailsController(EventEaseDbContext ctx) => _context = ctx;

        public async Task<IActionResult> Index(string search)
        {
            var query = _context.BookingDetails.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                // Search by Booking ID or Event Name
                bool isId = int.TryParse(search, out int bookingId);
                query = isId
                    ? query.Where(b => b.BookingId == bookingId)
                    : query.Where(b =>
                        b.EventName.Contains(search) ||
                        b.CustomerName.Contains(search) ||
                        b.VenueName.Contains(search));
            }

            ViewBag.CurrentSearch = search;
            return View(await query
                .OrderByDescending(b => b.EventDate).ToListAsync());
        }
    }

}
