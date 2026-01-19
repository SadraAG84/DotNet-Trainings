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
            return View();
        }
    }
}
