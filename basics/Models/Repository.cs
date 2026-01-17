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
                    Tags = new string[] { "aspnetcore", "web" }.Aggregate((a, b) => a + "," + b),
                    isActive = true,
                    isHome = true,
                },
                new Course
                {
                    ID = 2,
                    Title = "Course 2",
                    Description = "Description 2",
                    Image = "2.jpg",
                    Tags = new string[] { "aspnetcore2", "web2" }.Aggregate((a, b) => a + "," + b),
                    isActive = false,
                    isHome = true,
                },
                new Course
                {
                    ID = 3,
                    Title = "Course 3",
                    Description = "Description 3",
                    Image = "3.jpg",
                    Tags = new string[] { "aspnetcore3", "web3" }.Aggregate((a, b) => a + "," + b),
                    isActive = true,
                    isHome = false,
                },
            };
        }

        public static List<Course> Courses
        {
            get { return _courses; }
        }

        public static Course? GetCourseById(int? id)
        {
            return _courses.FirstOrDefault(c => c.ID == id);
        }
    }
}
