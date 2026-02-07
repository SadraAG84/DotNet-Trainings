using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UsersController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest();
            }

            // Create a new AppUser instance and populate it with data from the UserDTO (Creating the new user)
            // The UserDTO contains the necessary information for creating a user, such as FullName, UserName, Email, and Password
            // The AppUser class is a custom user class that inherits from IdentityUser<int> and includes additional properties like FullName and DateAdded
            var user = new AppUser
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                DateAdded = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User created successfully" });
            }
            // Return validation errors if user creation failed
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest();
            }

            // Find the user by their email address using the UserManager's FindByEmailAsync method
            // This method searches the user store for a user with the specified email and returns the user if found, or null if no user is found with that email
            // we use _userManager instead of _signInManager to find the user because _userManager provides methods for managing users, such as finding users by email, while _signInManager is focused on handling user sign-in and authentication processes
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            // Check if the provided password is correct for the found user
            // The CheckPasswordSignInAsync method checks the user's password and returns a SignInResult indicating whether the sign-in was successful or not
            // we use _signInManager instwead of _userManager to check the password because it provides additional functionality related to user sign-in, such as handling lockout and two-factor authentication
            var passwordValid = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Password,
                false // lockoutOnFailure: set to false to prevent locking out the user after failed login attempts
            );
            if (!passwordValid.Succeeded)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            // Here you would typically generate a JWT token and return it to the client
            return Ok(new { Message = "Login successful" });
        }
    }
}
