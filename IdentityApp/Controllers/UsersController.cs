using System.Threading.Tasks;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
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

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                var model = new EditViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName ?? string.Empty,
                    Email = user.Email,
                    SelectedRoles = await _userManager.GetRolesAsync(user),
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // Find the user by ID
                // Update the user's properties with the values from the model
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.FullName = model.FullName ?? string.Empty;
                    user.Email = model.Email;

                    // Update the user in the database

                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        // Update user roles
                        // First, remove all existing roles then add the selected roles from the model
                        await _userManager.RemoveFromRolesAsync(
                            user,
                            await _userManager.GetRolesAsync(user)
                        );
                        if (model.SelectedRoles != null)
                        {
                            await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                        }
                        // If a new password is provided, update the user's password
                        // This requires generating a password reset token since we don't have the old password
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            await _userManager.RemovePasswordAsync(user);
                            var passwordResult = await _userManager.AddPasswordAsync(
                                user,
                                model.Password
                            );

                            //---------------------------------------------------------------------------
                            // Generate a password reset token and use it to set the new password
                            // This is necessary because we cannot directly set the password without knowing the old one
                            //---------------------------------------------------------------------------
                            // var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            // var passwordResult = await _userManager.ResetPasswordAsync(
                            //     user,
                            //     token,
                            //     model.Password
                            // );

                            //---------------------------------------------------------------------------
                            // Check if the password reset was successful
                            // If there are errors, add them to the ModelState to display in the view
                            if (!passwordResult.Succeeded)
                            {
                                foreach (var error in passwordResult.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }
                                return View(model);
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
