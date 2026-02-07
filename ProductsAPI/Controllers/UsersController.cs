using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly IConfiguration _configuration;

        public UsersController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            return Ok(new { token = GenerateJwt(user) });
        }

        // This method generates a JSON Web Token (JWT) for the authenticated user
        private object GenerateJwt(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // Create an instance of JwtSecurityTokenHandler to handle the creation and manipulation of JWT tokens

            var key = Encoding.ASCII.GetBytes(
                _configuration.GetSection("Appsettings:Secret").Value ?? string.Empty
            ); // Retrieve the secret key from the configuration (appsettings.json) and convert it to a byte array using ASCII encoding. This key will be used to sign the JWT token, ensuring its integrity and authenticity. The secret key should be a long, random string to provide sufficient security for the token.

            var tokenDescriptor = new SecurityTokenDescriptor // Create a SecurityTokenDescriptor that describes the contents and properties of the JWT token to be generated
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Add a claim for the user's unique identifier (Id) using the ClaimTypes.NameIdentifier claim type. This allows the token to carry information about the user's identity, which can be used for authorization and authentication purposes.
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty), // Add a claim for the user's username using the ClaimTypes.Name claim type. This provides additional information about the user in the token, which can be useful for display purposes or for making authorization decisions based on the user's name.
                    }
                ),

                Expires = DateTime.UtcNow.AddDays(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ), // Set the signing credentials for the token using a symmetric security key created from the secret key and specifying the HMAC SHA256 algorithm for signing. This ensures that the token is securely signed and can be verified by the server when it is received in subsequent requests.
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); // Use the JwtSecurityTokenHandler to create a JWT token based on the provided token descriptor. The CreateToken method generates a token that includes the claims, expiration time, and signing credentials specified in the token descriptor.

            return tokenHandler.WriteToken(token);
        }
    }
}
