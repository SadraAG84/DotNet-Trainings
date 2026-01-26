using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace efcoreApp.Controllers
{
    public class StudentController : Controller
    {

        // Dependency Injection of DataContext to access the database Method
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            _context.Students.Add(model);
            await  _context.SaveChangesAsync();
            RedirectToAction("Index", "Home");
        }
    }
}
