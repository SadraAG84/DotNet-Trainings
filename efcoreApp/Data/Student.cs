using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Student
    {
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Student First Name")]
        public string? StudentFirstName { get; set; }
        [Display(Name = "Student Last Name")]
        public string? StudentLastName { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
