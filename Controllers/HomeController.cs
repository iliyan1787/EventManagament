using EventManagamentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventManagamentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index action with dynamic content
        public IActionResult Index()
        {
            // Sample data for the homepage
            var homes = new List<Home>
            {
                new Home { Id = 1, Title = "Discover Events Near You", Description = "Stay updated with the latest events happening in your city!" },
                new Home { Id = 2, Title = "Plan Your Event", Description = "Organize and manage your events effortlessly with our system." },
                new Home { Id = 3, Title = "Feedback Matters", Description = "Share your thoughts and help us improve!" }
            };

            var highlights = new List<string>
            {
                "100+ events hosted successfully",
                "20,000+ attendees engaged",
                "24/7 support for event organizers"
            };

            var viewModel = new HomeViewModel
            {
                Homes = homes,
                Highlights = highlights
            };

            return View(viewModel);
        }

        // Privacy policy page
        public IActionResult Privacy()
        {
            ViewData["PrivacyPolicy"] = "Your privacy is important to us. We are committed to safeguarding your personal information.";
            return View();
        }

        // Contact page
        public IActionResult Contact()
        {
            ViewData["ContactInfo"] = "Have questions or need support? Reach us at support@eventmanagement.com or call +1-800-EVENTS.";
            return View();
        }

        // About page with team info
        public IActionResult About()
        {
            var teamMembers = new List<string>
            {
                "John Doe - Founder & CEO",
                "Jane Smith - Chief Marketing Officer",
                "Alice Brown - Lead Developer"
            };

            ViewData["Team"] = teamMembers;
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            _logger.LogError("An error occurred. Request ID: {RequestId}", errorModel.RequestId);

            return View(errorModel);
        }
    }
    public class HomeViewModel
    {
        public List<Home> Homes { get; set; }
        public List<string> Highlights { get; set; }
    }
}