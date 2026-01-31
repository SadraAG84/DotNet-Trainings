using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public string? InstructorFirstName { get; set; }
        public string? InstructorLastName { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartingDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
