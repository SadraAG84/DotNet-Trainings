using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class InstructorController : Controller
    {
        private readonly DataContext _context;

        public InstructorController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var instructor = await _context.Instructors.ToListAsync();
            return View(instructor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Instructor model)
        {
            _context.Instructors.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(s =>
                s.InstructorId == id
            );
            // var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor model)
        {
            if (id != model.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Instructors.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instructors.Any(s => s.InstructorId == model.InstructorId))
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
            var instructor = await _context.Instructors.FindAsync(id);
            // var instructor = await _context.Instructors.FirstOrDefaultAsync(s => s.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
