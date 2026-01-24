using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class MeetingController : Controller
    {
        // localhost/Meeting
        // localhost/Meeting/Index

        // we replaced Index.cs with Thanks.cs so we do not need this action anymore
        // [HttpGet]
        // public IActionResult Index()
        // {
        //     return View();
        // }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserInfo model)
        {
            // Console.WriteLine($"Name: {model.Name}");
            // Console.WriteLine($"Email: {model.Email}");
            // Console.WriteLine($"Phone: {model.Phone}");
            // Console.WriteLine($"Will Attend: {model.WillAttend}");

            // data can be saved to database here
            // we will list all registered users in the List view
            if (ModelState.IsValid)
            {
                Repository.CreateUser(model);
                ViewBag.UserCount = Repository.Users.Where(info => info.WillAttend == true).Count();
                return View("Thanks", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(Repository.Users);
        }

        // localhost/Meeting/Details/Id
        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = Repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
