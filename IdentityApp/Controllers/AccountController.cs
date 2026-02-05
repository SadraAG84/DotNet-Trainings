using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IdentityApp.ViewModels.LoginViewModel model)
        {
            // Check if the model state is valid

            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // Sign out any existing user
                    await _signInManager.SignOutAsync();

                    // Attempt to sign in
                    var result = await _signInManager.PasswordSignInAsync(
                        user,
                        model.Password,
                        model.RememberMe,
                        true
                    );

                    if (result.Succeeded)
                    {
                        // Reset access failed count on successful login
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        // Account is locked out
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError(
                            string.Empty,
                            $"Account locked out. Try again in {timeLeft.Minutes} minutes and {timeLeft.Seconds} seconds."
                        );
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid password.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "NO user found with this email.");
                }
            }

            return View(model);
        }
    }
}
