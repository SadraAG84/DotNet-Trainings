using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();

        static Repository()
        {
            _courses = new List<Course>
            {
                new Course
                {
                    ID = 1,
                    Title = "Course 1",
                    Description = "Description 1",
                    Image = "1.jpg",
                },
                new Course
                {
                    ID = 2,
                    Title = "Course 2",
                    Description = "Description 2",
                    Image = "2.jpg",
                },
                new Course
                {
                    ID = 3,
                    Title = "Course 3",
                    Description = "Description 3",
                    Image = "3.jpg",
                },
            };
        }

        public static List<Course> Courses
        {
            get { return _courses; }
        }
    }
}
