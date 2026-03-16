using Microsoft.AspNetCore.Mvc;
using EventEaseVMS.Data;
using EventEaseVMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEaseVMS.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly EventEaseDbContext _context;

        public EventTypeController(EventEaseDbContext context)
        {
            _context = context;
        }
        //Get EventType
        public async Task<IActionResult> Index()
        {
            var eventTypes = await _context.EventTypes.ToListAsync();
            return View(eventTypes);
        }
        //Get EventType
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventType eventType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventType);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var eventType = await _context.EventTypes.FindAsync(id);
            if (eventType == null) return NotFound();
            return View(eventType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventType eventType)
        {
            if (ModelState.IsValid)
            {
                _context.Update(eventType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventType);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var eventType = await _context.EventTypes.FindAsync(id);
            if (eventType == null) return NotFound();
            return View(eventType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventType = await _context.EventTypes.FindAsync(id);
            _context.EventTypes.Remove(eventType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventType = await _context.EventTypes.FindAsync(id);
            if (eventType == null) return NotFound();
            return View(eventType);
        }
    }

}
