using EventManagamentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
public class EventController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var events = _context.Events.Include(e => e.Organizer).ToList();
        return View(events);
    }

    [HttpGet("Details/{id}")]
    public IActionResult Details(int id)
    {
        var eventDetail = _context.Events.Include(e => e.Organizer).FirstOrDefault(e => e.EventId == id);
        if (eventDetail == null)
        {
            return NotFound();
        }
        return View(eventDetail);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Event newEvent)
    {
        if (ModelState.IsValid)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(newEvent);
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var eventToEdit = _context.Events.Find(id);
        if (eventToEdit == null)
        {
            return NotFound();
        }
        return View(eventToEdit);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Event updatedEvent)
    {
        if (id != updatedEvent.EventId || !ModelState.IsValid)
        {
            return View(updatedEvent);
        }
        _context.Events.Update(updatedEvent);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var eventToDelete = _context.Events.Find(id);
        if (eventToDelete == null)
        {
            return NotFound();
        }
        return View(eventToDelete);
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var eventToDelete = _context.Events.Find(id);
        if (eventToDelete != null)
        {
            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
