using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppRole model)
        {
            if (ModelState.IsValid)
            {
                var role = new AppRole { Name = model.Name };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // Edit Role
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null && role.Name != null)
            {
                ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                return View(role);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppRole model)
        {
            if (ModelState.IsValid)
            {
                // Find the role by ID
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    // Update the role name
                    role.Name = model.Name;
                    IdentityResult result = await _roleManager.UpdateAsync(role);

                    // Check if the update was successful
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    // Add errors to ModelState if update failed
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                // Role not found
                if (role != null && role.Name != null)
                {
                    ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                }
                else
                {
                    return NotFound();
                }
            }
            return View(model);
        }
    }
}
