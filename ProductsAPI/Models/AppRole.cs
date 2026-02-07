using Microsoft.AspNetCore.Identity;

namespace ProductsAPI.Models
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; } = string.Empty;
    }
}
