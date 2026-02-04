using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class EditViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }

        public string FullName { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        // List of roles selected for the user  
        public IList<string>? SelectedRoles { get; set; }
    }
}
