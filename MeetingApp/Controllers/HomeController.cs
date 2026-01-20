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
            ViewBag.User = "Sadra";

            return View();
        }
    }
}
