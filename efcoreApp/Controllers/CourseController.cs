namespace efcoreApp.Controllers
{
    using System.Threading.Tasks;
    using efcoreApp.Data;
    using efcoreApp.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CourseController : Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(k => k.Instructor).ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Instructors = new SelectList(
                await _context.Instructors.ToListAsync(),
                "InstructorId",
                "NameLastname"
            );
            ViewBag.Instructors = new SelectList(
                await _context.Instructors.ToListAsync(),
                "InstructorId",
                "NameLastname"
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = new SelectList(
                    await _context.Instructors.ToListAsync(),
                    "InstructorId",
                    "NameLastname"
                );
                return View(model);
            }

            // Check if InstructorId exists
            var instructorExists = await _context.Instructors.AnyAsync(i =>
                i.InstructorId == model.InstructorId
            );
            if (!instructorExists)
            {
                ModelState.AddModelError("InstructorId", "Selected instructor does not exist.");
                ViewBag.Instructors = new SelectList(
                    await _context.Instructors.ToListAsync(),
                    "InstructorId",
                    "NameLastname"
                );
                return View(model);
            }

            _context.Courses.Add(
                new Course()
                {
                    CourseId = model.CourseId,
                    CourseName = model.CourseName,
                    InstructorId = (int)model.InstructorId!,
                }
            );
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var course = await _context
                .Courses.Include(c => c.CourseEnrollments)
                    .ThenInclude(c => c.Student)
                .Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    InstructorId = c.InstructorId,
                    CourseEnrollments = c.CourseEnrollments,
                })
                .FirstOrDefaultAsync(c => c.CourseId == id);
            // var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Instructors = new SelectList(
                await _context.Instructors.ToListAsync(),
                "InstructorId",
                "NameLastname"
            );
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel model)
        {
            if (id != model.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(
                        new Course()
                        {
                            CourseId = model.CourseId,
                            CourseName = model.CourseName,
                            InstructorId = (int)model.InstructorId!,
                        }
                    );
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Courses.Any(c => c.CourseId == model.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var course = await _context.Courses.FindAsync(id);
            // var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
