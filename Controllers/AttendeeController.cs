using EventManagamentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class AttendeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public AttendeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var attendees = _context.Attendees.Include(a => a.Event).Include(a => a.User);
        return View(await attendees.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var attendee = await _context.Attendees.Include(a => a.Event).Include(a => a.User)
            .FirstOrDefaultAsync(a => a.AttendeeId == id);
        if (attendee == null)
        {
            return NotFound();
        }
        return View(attendee);
    }

    public IActionResult Create()
    {
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Attendee attendee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(attendee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title", attendee.EventId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", attendee.UserId);
        return View(attendee);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var attendee = await _context.Attendees.FindAsync(id);
        if (attendee == null)
        {
            return NotFound();
        }
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title", attendee.EventId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", attendee.UserId);
        return View(attendee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Attendee attendee)
    {
        if (id != attendee.AttendeeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(attendee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Attendees.Any(e => e.AttendeeId == attendee.AttendeeId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title", attendee.EventId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", attendee.UserId);
        return View(attendee);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var attendee = await _context.Attendees.Include(a => a.Event).Include(a => a.User)
            .FirstOrDefaultAsync(a => a.AttendeeId == id);
        if (attendee == null)
        {
            return NotFound();
        }
        return View(attendee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var attendee = await _context.Attendees.FindAsync(id);
        if (attendee != null)
        {
            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
