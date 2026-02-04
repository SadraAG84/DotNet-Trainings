using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? InstructorId { get; set; }

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; } =
            new List<CourseEnrollment>();
    }
}
