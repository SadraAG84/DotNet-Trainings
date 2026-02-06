using IdentityApp.Models;
using IdentityApp.ViewModels;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

                    // Check if email is confirmed
                    // if (!await _userManager.IsEmailConfirmedAsync(user))
                    // {
                    //     // Email not confirmed
                    //     ModelState.AddModelError(
                    //         string.Empty,
                    //         "Email not confirmed. Please confirm your email before logging in."
                    //     );
                    //     return View(model);
                    // }

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
                    ModelState.AddModelError(string.Empty, "No user found with this email.");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, token = token }
                    );
                    TempData["Message"] = "Please check your email to confirm your account.";
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // public async Task<IActionResult> ConfirmEmail(string userId, string token)
        // {
        //     if (userId == null || token == null)
        //     {
        //         TempData["Message"] = "Invalid token";
        //         return View();
        //     }

        //     var user = await _userManager.FindByIdAsync(userId);
        //     if (user == null)
        //     {
        //         return NotFound($"Unable to load user with ID '{userId}'.");
        //     }

        //     var result = await _userManager.ConfirmEmailAsync(user, token);
        //     if (result.Succeeded)
        //     {
        //         TempData["Message"] = "Email confirmed successfully";
        //         return RedirectToAction("Login", "Account");
        //     }
        //     else
        //     {
        //         TempData["Message"] = "Error confirming your email.";
        //         return View();
        //     }
        // }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(email);

                // If the user exists and email is confirmed( && await _userManager.IsEmailConfirmedAsync(user)) ("we are not checking confirmation yet"), generate a password reset token
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action(
                        "ResetPassword",
                        "Account",
                        new { token = token, email = user.Email },
                        Request.Scheme
                    );

                    // Here, you would typically send the resetLink via email to the user.
                    // For demonstration purposes, we'll just display it on the screen.
                    ViewBag.ResetLink = resetLink;
                    return View("ForgotPasswordConfirmation");
                }
                // To prevent account enumeration, we display the same confirmation message
                return View("ForgotPasswordConfirmation");
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string Id)
        {
            if (token == null || Id == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset token.");
            }
            var model = new ResetPasswordModel { Token = token, Email = Id };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(
                        user,
                        model.Token,
                        model.Password
                    );
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    // To prevent account enumeration, we display the same confirmation message
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }
    }
}
