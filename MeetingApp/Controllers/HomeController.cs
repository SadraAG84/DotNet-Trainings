using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
        // localhost
        // localhost/Home
        // localhost/Home/Index
        public string Index()
        {
            return "This is Home/Index";
        }
    }
}
