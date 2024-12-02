using EventManagamentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class FeedbackController : Controller
{
    private readonly ApplicationDbContext _context;

    public FeedbackController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var feedbacks = _context.Feedbacks.Include(f => f.Event).Include(f => f.User);
        return View(await feedbacks.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var feedback = await _context.Feedbacks.Include(f => f.Event).Include(f => f.User)
            .FirstOrDefaultAsync(f => f.FeedbackId == id);
        if (feedback == null)
        {
            return NotFound();
        }
        return View(feedback);
    }

    public IActionResult Create()
    {
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            _context.Add(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title", feedback.EventId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", feedback.UserId);
        return View(feedback);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title", feedback.EventId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", feedback.UserId);
        return View(feedback);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Feedback feedback)
    {
        if (id != feedback.FeedbackId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(feedback);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Feedbacks.Any(e => e.FeedbackId == feedback.FeedbackId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Title", feedback.EventId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", feedback.UserId);
        return View(feedback);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var feedback = await _context.Feedbacks.Include(f => f.Event).Include(f => f.User)
            .FirstOrDefaultAsync(f => f.FeedbackId == id);
        if (feedback == null)
        {
            return NotFound();
        }
        return View(feedback);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback != null)
        {
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
