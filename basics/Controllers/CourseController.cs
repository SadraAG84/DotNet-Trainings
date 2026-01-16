using basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace basics.Controllers;

// /course
public class CourseController : Controller
{
    // /course or /course/index
    public IActionResult Index()
    {
        var course = new Course();
        course.ID = 1;
        course.Title = "Aspnet course";
        course.Image = "1.jpg";
        return View(course);
    }

    public IActionResult Details()
    {
        var course = new Course();
        course.ID = 1;
        course.Title = "Aspnet course";
        course.Image = "1.jpg";
        return View(course);
    }

    // /course/list
    public IActionResult List()
    {
        return View(Repository.Courses);
    }
}
