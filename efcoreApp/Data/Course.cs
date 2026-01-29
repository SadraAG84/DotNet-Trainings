using efcoreApp.Controllers;

namespace efcoreApp.Data
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public int? InstructorId { get; set; }

        public Instructor Instructor { get; set; } = null!;

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; } =
            new List<CourseEnrollment>();
    }
}
