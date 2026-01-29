using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateTime StartingDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
