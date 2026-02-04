// Define application-specific user properties by extending IdentityUser
// Here we add a FullName property to store the user's full name
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
