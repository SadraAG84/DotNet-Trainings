using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
        // localhost
        // localhost/Home
        // localhost/Home/Index
        public IActionResult Index()
        {
            int hour = System.DateTime.Now.Hour;

            // ViewData["Greeting"] = hour < 12 ? "Good Morning" : "Good Afternoon";

            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            // ViewBag.User = "Sadra";

            var meetingInfo = new MeetingInfo()
            {
                Id = 1,
                Title = "Project Meeting",
                Location = "Room 101",
                Date = new DateTime(2026, 01, 20, 10, 0, 0),
                NumberOfPeople = 25,
            };

            return View(meetingInfo);
        }
    }
}
