using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventEaseVMS.Data;
using EventEaseVMS.Models;
using Microsoft.EntityFrameworkCore;


namespace EventEaseVMS.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly EventEaseDbContext _context;
        public BookingDetailsController(EventEaseDbContext ctx) => _context = ctx;

        public async Task<IActionResult> Index(
            string search,
            int? eventTypeId,
            DateTime? dateFrom,
            DateTime? dateTo,
            bool? availableOnly)
        {
            var query = _context.BookingDetails.AsQueryable();

            // Search by BookingID or Event Name / Customer / Venue
            if (!string.IsNullOrEmpty(search))
            {
                bool isId = int.TryParse(search, out int bookingId);
                query = isId
                    ? query.Where(b => b.BookingId == bookingId)
                    : query.Where(b =>
                        b.EventName.Contains(search) ||
                        b.CustomerName.Contains(search) ||
                        b.VenueName.Contains(search));
            }

            // Filter by Event Type
            if (eventTypeId.HasValue)
                query = query.Where(b => b.EventTypeName ==
                    _context.EventTypes.Where(e => e.EventTypeId == eventTypeId)
                    .Select(e => e.TypeName).FirstOrDefault());

            // Filter by Date Range
            if (dateFrom.HasValue)
                query = query.Where(b => b.EventDate >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(b => b.EventDate <= dateTo.Value);

            // Pass filter data to view
            ViewBag.EventTypes = new SelectList(
                _context.EventTypes.OrderBy(e => e.TypeName),
                "EventTypeId", "TypeName", eventTypeId);
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentType = eventTypeId;
            ViewBag.CurrentFrom = dateFrom?.ToString("yyyy-MM-dd");
            ViewBag.CurrentTo = dateTo?.ToString("yyyy-MM-dd");

            var results = await query
                .OrderByDescending(b => b.EventDate).ToListAsync();
            return View(results);
        }
    }
}


   
