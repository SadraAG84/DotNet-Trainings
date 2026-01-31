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
    }
}
