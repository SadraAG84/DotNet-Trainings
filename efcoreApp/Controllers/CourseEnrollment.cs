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
            var courseEnrollments = await _context
                .CourseEnrollments.Include(x => x.Student)
                .Include(x => x.Course)
                .ToListAsync();
            return View(courseEnrollments);
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var courseEnrollment = await _context.CourseEnrollments.FindAsync(id);
            if (courseEnrollment == null)
                return NotFound();

            _context.CourseEnrollments.Remove(courseEnrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
