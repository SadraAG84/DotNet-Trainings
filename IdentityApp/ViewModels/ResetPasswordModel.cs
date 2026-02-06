using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class ResetPasswordModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
