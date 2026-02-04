using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user with the provided username and email, and set the password
                // The UserManager will handle password hashing and user creation
                // If the user creation is successful, redirect to the Index action to show the list of users
                // If there are errors during user creation, add them to the ModelState to display in the view
                // Note: The UserManager will automatically handle password hashing and user creation
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                // The following line checks if the username (or email) is already taken:
                // If the username is already taken, result.Succeeded will be false, and result.Errors will contain an error describing the issue.
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
    }
}
