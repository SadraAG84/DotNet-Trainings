using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class CourseEnrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
