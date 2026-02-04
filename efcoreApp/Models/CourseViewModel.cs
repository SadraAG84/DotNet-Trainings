using System.ComponentModel.DataAnnotations;
using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Course name cannot be longer than 50 characters.")]
        public string? CourseName { get; set; }

        [Required(ErrorMessage = "You should select an instructor")]
        public int? InstructorId { get; set; }

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; } =
            new List<CourseEnrollment>();
    }
}
