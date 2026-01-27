namespace efcoreApp.Controllers
{
    using efcoreApp.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CourseEnrollmentController : Controller
    {
        private readonly DataContext _context;

        public CourseEnrollmentController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = new SelectList(
                await _context.Students.ToListAsync(),
                "StudentId",
                "NameLastname"
            );
            ViewBag.Courses = new SelectList(
                await _context.Courses.ToListAsync(),
                "CourseId",
                "CourseName"
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseEnrollment model)
        {
            model.EnrollmentDate = DateTime.Now;
            _context.CourseEnrollments.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
