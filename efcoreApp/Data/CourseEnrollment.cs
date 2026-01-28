using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class CourseEnrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public DateTime EnrollmentDate { get; set; }
    }
}
