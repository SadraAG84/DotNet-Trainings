using MeetingApp.Models;
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
        public IActionResult Register(UserInfo userInfo)
        {
            Console.WriteLine($"Name: {userInfo.Name}");
            Console.WriteLine($"Email: {userInfo.Email}");
            Console.WriteLine($"Phone: {userInfo.Phone}");
            Console.WriteLine($"Will Attend: {userInfo.WillAttend}");
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
    }
}
