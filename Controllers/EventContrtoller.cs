using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EventManagamentSystem.Models;

namespace EventManagamentSystem.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            var events = new List<Event>
            {
                new Event
                {
                    EventId = 1,
                    Title = "Intergalactic Food Festival",
                    Location = "Mars Colony Delta",
                    StartDate = new DateTime(2030, 5, 21),
                    Description = "Taste culinary delights from across the galaxy!"
                },
                new Event
                {
                    EventId = 2,
                    Title = "Time Travelers' Conference",
                    Location = "Temporal Nexus",
                    StartDate = new DateTime(2077, 1, 15),
                    Description = "Join discussions with explorers from past, present, and future."
                },
                new Event
                {
                    EventId = 3,
                    Title = "Underwater Symphony",
                    Location = "Pacific Ocean Dome",
                    StartDate = new DateTime(2025, 11, 10),
                    Description = "Experience music like never before in a breathtaking underwater setting."
                },
                new Event
                {
                    EventId = 4,
                    Title = "Artificial Intelligence Art Exhibition",
                    Location = "Silicon Valley",
                    StartDate = new DateTime(2024, 8, 20),
                    Description = "Explore creative masterpieces crafted by AI from around the world."
                }
            };

            return View(events);
        }
    }
}
