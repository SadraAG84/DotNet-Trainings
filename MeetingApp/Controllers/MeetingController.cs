using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class MeetingController : Controller
    {
        // localhost/Meeting
        // localhost/Meeting/Index

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Name, string Email, string Phone, bool WillAttend)
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Will Attend: {WillAttend}");
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
    }
}
