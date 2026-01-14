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
        var courses = new List<Course>()
        {
            new Course()
            {
                ID = 1,
                Title = "Course 1",
                Description = "Course 1 Description",
                Image = "1.jpg",
            },
            new Course()
            {
                ID = 2,
                Title = "Course 2",
                Description = "Course 2 Description",
                Image = "2.jpg",
            },
            new Course()
            {
                ID = 3,
                Title = "Course 3",
                Description = "Course 3 Description",
                Image = "3.JPEG",
            },
        };

        return View(courses);
    }
}
